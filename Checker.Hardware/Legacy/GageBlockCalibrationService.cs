using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class GageBlockCalibrationService
    {
        private const double CountsPerInch = 254000.0;

        private readonly IEncoder _encoder;
        private readonly ILoadCell _loadCell;
        private readonly IMotionController _motionController;
        private readonly IEncoderCalibrationStore _calibrationStore;

        public GageBlockCalibrationService(IEncoder encoder, ILoadCell loadCell, IMotionController motionController, IEncoderCalibrationStore calibrationStore)
        {
            _encoder = encoder;
            _loadCell = loadCell;
            _motionController = motionController;
            _calibrationStore = calibrationStore;   
        }

        public async Task<double> FindGageBlockAsync(CancellationToken cancellationToken = default)
        {
            var baselineCounts = _loadCell.ReadAverageRaw(16);

            _loadCell.SetBaseline(baselineCounts);

            var targetForceCounts = baselineCounts + 400;

            await _motionController.MoveDownAsync(0.025);

            while (!cancellationToken.IsCancellationRequested)
            {
                var currentCounts = _loadCell.ReadAverageRaw(16);

                _encoder.LatchPosition();

                if(currentCounts >= targetForceCounts)
                {
                    var blockFoundAt = _encoder.ReadPosition();

                    await _motionController.StopAsync();
                    await _motionController.RaiseHeadAsync(0.05);

                    return blockFoundAt;
                }

                await Task.Delay(1, cancellationToken);
            }

            throw new OperationCanceledException(cancellationToken);
        }

        public void AdjustEncoderOffsetFromGageBlock(double blockFoundAt, double actualBlockValue)
        {
            if (actualBlockValue >= 4.1)
                throw new ArgumentOutOfRangeException("Gage Block value must be less than 4.1 inches");

            var calibration = _calibrationStore.Load();
            var encoderOffsetCounts = calibration.EncoderOffsetCounts;

            if(actualBlockValue > blockFoundAt)
            {
                var difference = actualBlockValue - blockFoundAt;

                encoderOffsetCounts = (long)Math.Truncate(difference * CountsPerInch);
            }
            else
            {
                var difference = blockFoundAt - actualBlockValue;

                encoderOffsetCounts = (long)Math.Truncate(difference * CountsPerInch);
            }

            _encoder.SetEncoderOffset(encoderOffsetCounts);
        }

        public void SetPositionFromGageBlock(double blockFoundAt, double actualBlockValue)
        {
            if (actualBlockValue >= 4.1)
                throw new ArgumentOutOfRangeException("Gage block value must be less than 4.1 inches.");

            _encoder.LatchPosition();

            var currentRawCounts = _encoder.ReadCounts();

            var currentCalculatedOffset = currentRawCounts + (long)Math.Truncate(blockFoundAt * CountsPerInch);

            long adjustedOffset;

            if(actualBlockValue > blockFoundAt)
            {
                var diff = actualBlockValue - blockFoundAt;
                adjustedOffset = currentCalculatedOffset + (long)Math.Truncate(diff * CountsPerInch);
            }
            else
            {
                var diff = blockFoundAt - actualBlockValue;

                adjustedOffset = currentCalculatedOffset - (long)Math.Truncate(diff * CountsPerInch);
            }

            _encoder.SetEncoderOffset(adjustedOffset);
        }

        public void PresetHeadPositionToFourInches()
        {
            var encoderOffsetCounts = (long)Math.Truncate(4.0 * CountsPerInch);

            _encoder.SetEncoderOffset(encoderOffsetCounts);
        }
    }
}
