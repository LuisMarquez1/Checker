using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyCalibrationController
    {
        private const double CountsPerInch = 254000.0;

        private const int GageBlockTouchDeltaCounts = 400;

        private readonly IEncoder _encoder;
        private readonly LegacyDas1402LoadCell _loadCell;
        private readonly LegacyLacController _lac;
        private readonly LegacyPcdio120Controller _pcdio;

        private bool _motorPowered = true;

        public LegacyCalibrationController(IEncoder encoder, LegacyDas1402LoadCell loadCell, LegacyLacController lac, LegacyPcdio120Controller pcdio)
        {
            _encoder = encoder;
            _loadCell = loadCell;
            _lac = lac;
            _pcdio = pcdio;
        }

        public int RetakeBaseline()
        {
            return _loadCell.RetakeBaseline();
        }

        public LoadCellCalibrationSnapshot ReadLoadCellSnapshot()
        {
            return _loadCell.ReadCalibrationSnapshot();
        }

        public DasRange ToggleLoadCellRange()
        {
            return _loadCell.ToggleRange();
        }

        public long ReadEncoderCounts()
        {
            _encoder.LatchPosition();

            return _encoder.ReadCounts();
        }

        public double ReadEncoderPosition()
        {
            _encoder.LatchPosition();

            return _encoder.ReadPosition();
        }

        public async Task<double> FindGageBlockAsync(CancellationToken cancellationToken = default)
        {
            var baselineCounts = _loadCell.ReadAverageRaw(16);

            _loadCell.SetBaseline(baselineCounts);

            var targetCounts = baselineCounts + GageBlockTouchDeltaCounts;

            await _lac.MoveDownAsync(0.025);

            while (!cancellationToken.IsCancellationRequested)
            {
                var currentCounts = _loadCell.ReadAverageRaw(16);

                _encoder.LatchPosition();

                if (currentCounts >= targetCounts)
                {
                    var blockFoundAt = _encoder.ReadPosition();

                    await _lac.StopAsync();

                    await _lac.RaiseHeadAsync(0.05);

                    return blockFoundAt;
                }

                await Task.Delay(1, cancellationToken);
            }

            throw new OperationCanceledException(cancellationToken);
        }

        public void AdjustEncoderOffsetFromGageBlock(double blockFoundAt, double actualBlockValue)
        {
            if (actualBlockValue >= 4.1)
                throw new ArgumentOutOfRangeException(nameof(actualBlockValue), "Gage block value must be less than 4.1 inches.");

            _encoder.LatchPosition();

            var rawCounts = _encoder.ReadCounts();

            var currentOffset = rawCounts + (long)Math.Truncate(blockFoundAt * CountsPerInch);

            long adjustedOffset;

            if (actualBlockValue > blockFoundAt)
            {
                var difference = actualBlockValue - blockFoundAt;

                adjustedOffset = currentOffset + (long)Math.Truncate(difference * CountsPerInch);
            }
            else
            {
                var difference = blockFoundAt - actualBlockValue;

                adjustedOffset = currentOffset - (long)Math.Truncate(difference * CountsPerInch);
            }

            _encoder.SetEncoderOffset(adjustedOffset);
        }

        public void PresetHeadPositionToFourInches()
        {
            const long fourInchOffsetCounts = 1_016_00;

            _encoder.SetEncoderOffset(fourInchOffsetCounts);
        }

        public async Task HeadUp100Async()
        {
            await _lac.HeadUp100Async();
        }

        public async Task HeadDown100Async()
        {
            await _lac.HeadDown100Async();
        }

        public void ToggleNest()
        {
            _pcdio.ToggleNest();
        }

        public async Task ToggleMotorPowerAsync()
        {
            if (_motorPowered)
            {
                await _lac.MotorOffAsync();

                _motorPowered = false;
            }
            else
            {
                await _lac.MotorOnAsync();

                _motorPowered = true;
            }
        }

        public async Task MotorOnAsync()
        {
            await _lac.MotorOnAsync();

            _motorPowered = true;
        }

        public async Task MotorOffAsync()
        {
            await _lac.MotorOffAsync();

            _motorPowered = false;
        }
    }
}