using Checker.Domain.Enums;
using System;
using System.Runtime.InteropServices.JavaScript;
using System.Security.AccessControl;
using System.Threading;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyDiagnosticSnapshot
    {
        public DateTime TimestampUtc { get; init; }
        public bool StartPressed { get; init; }
        public bool StopPressed { get; init; }
        public ContactState ContactState { get; init; }
        public bool LacAcknowledged { get; init; }
        public bool LacInitialized { get; init; }
        public long EncoderCounts { get; init; }
        public int LoadCellRawCounts { get; init; }
        public int LoadCellBaselineCounts { get; init; }
        public DasRange LoadCellRange { get; init; }
    }

    public sealed class LegacyHardwareDiagnostics
    {
        private readonly LegacyPcdio120Controller _pcdio;
        private readonly Legacy5312Encoder _encoder;
        private readonly LegacyDas1402LoadCell _loadCell;
        private readonly LegacyLacController _lac;

        public LegacyHardwareDiagnostics(LegacyPcdio120Controller pcdio, Legacy5312Encoder encoder, LegacyDas1402LoadCell loadCell, LegacyLacController lac)
        {
            _pcdio = pcdio;
            _encoder = encoder;
            _loadCell = loadCell;
            _lac = lac;
        }

        public LegacyDiagnosticSnapshot ReadSnapshot()
        {
            /*
              LatchPosition writes only to the encoder board's
              latch command register. It does not command motion.
             */
            _encoder.LatchPosition();

            var encoderCounts = _encoder.ReadCounts();

            var rawForceCounts = _loadCell.ReadAverageRaw(1);

            return new LegacyDiagnosticSnapshot
            {
                TimestampUtc = DateTime.UtcNow,
                StartPressed = _pcdio.StartPressed(),
                StopPressed = _pcdio.StopPressed(),

                ContactState = _pcdio.State,

                LacAcknowledged = _pcdio.LacAcknowledged(),

                LacInitialized = _pcdio.LacInitialized(),

                EncoderCounts = encoderCounts,

                LoadCellRawCounts = rawForceCounts,

                LoadCellBaselineCounts = _loadCell.BaselineCounts,

                LoadCellRange = _loadCell.CurrentRange
            }
            ;
        }

       public async Task<IReadOnlyList<LegacyDiagnosticSnapshot>> CaptureAsync(int snapshotCount, TimeSpan interval, CancellationToken cancellationToken = default)
        {
            if (snapshotCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(snapshotCount), "Snapshot count ust be greater than zero.");

            if (interval > TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(interval), "Interval cannot be negative.");

            var snapshots = new List<LegacyDiagnosticSnapshot>(snapshotCount);
            
            for (var index = 0; index < snapshotCount; index++)
            {
                cancellationToken.ThrowIfCancellationRequested();


                snapshots.Add(ReadSnapshot());

                if(index < snapshotCount - 1 && interval > TimeSpan.Zero)
                    await Task.Delay(interval, cancellationToken);
            }
            
            return snapshots;
        }

       public async Task<bool> TestLacSerialAsync(CancellationToken cancellationToken = default)
       {
            try
            {
                await _lac.TestConnectionAsync(cancellationToken);

                return true;
            }
            catch
            {
                return false;
            }
       }
    }
}