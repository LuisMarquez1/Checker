using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyHardwareInitializer
    {
        private readonly LegacyPcdio120Controller _pcdio;
        private readonly Legacy5312Encoder _encoder;
        private readonly LegacyDas1402LoadCell _loadCell;
        private readonly LegacyLacController _lac;
        private readonly LegacyCalibrationController _calibrationController;

        private bool _initialized;

        public LegacyHardwareInitializer(
            LegacyPcdio120Controller pcdio,
            Legacy5312Encoder encoder,
            LegacyDas1402LoadCell loadCell,
            LegacyLacController lac,
            LegacyCalibrationController calibrationController)
        {
            _pcdio = pcdio;
            _encoder = encoder;
            _loadCell = loadCell;
            _lac = lac;
            _calibrationController = calibrationController;
        }

        public bool IsInitialized => _initialized;

        public async Task InitializeAsync(string lacProgramPath, long? storedEncoderOffsetCounts, CancellationToken cancellationToken = default)
        {
            if(_initialized) return;

            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                // Initialize PCDIO120 Board
                _pcdio.Initialize();

                // Initialize 5312 Encoder
                _encoder.Initialize();

                // Initialize AtoD board
                _loadCell.Initialize();

                // Initialize Serial Module + SetupLAC
                await _lac.SetupAsync(lacProgramPath, cancellationToken);

                // Download LAC variables
                await _lac.DownloadVariablesAsync(cancellationToken);

                // Start LAC and Initialize LAC
                await _lac.InitializeToTopLimitAsync(cancellationToken);

                // Original software (PASCAL) zero the encoder after reaching the top refrence.
                _encoder.Zero();

                // Restore calibrated offset from machine configuraciopn.
                // This value originates from gage-block calibration.
                if(storedEncoderOffsetCounts.HasValue)
                    _encoder.SetEncoderOffset(storedEncoderOffsetCounts.Value);

                // Original Sequence:
                await _lac.LowerHeadAsync(0.1);

                await Task.Delay(200, cancellationToken);

                // LAC command(DefineHome)
                await _lac.DefineHomeAsync(cancellationToken);

                // Initialize LoadCell baseLine
                _calibrationController.RetakeBaseline();

                // Safe default state after initialization.
                _pcdio.LowerNest();
                _pcdio.TurnOtRelayOff();

                _initialized = true;
            }
            catch
            {
                await TryStopSafelyAsync();

                _pcdio.LowerNest();
                _pcdio.TurnOtRelayOff();

                _initialized = false;

                throw;
            }
        }

        public async Task ShutdownAsync()
        {
            await TryStopSafelyAsync();

            _pcdio.LowerNest();
            _pcdio.TurnOtRelayOff();

            _initialized = false;
        }

        private async Task TryStopSafelyAsync()
        {
            try
            {
                await _lac.StopAsync();
            }
            catch (Exception)
            {
                // Initialization may fail before LAC is ready.
                // Continue forcing the remaining outputs to safe state.
                throw;
            }
        }
    }
}
