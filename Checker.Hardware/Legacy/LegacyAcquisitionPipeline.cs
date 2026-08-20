using System.Diagnostics;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyAcquisitionPipeline: IAcquisitionPipeline
    {
        private readonly IEncoder _encoder;
        private readonly ILoadCell _loadCell;
        private readonly IContactMonitor _contactMonitor;

        public LegacyAcquisitionPipeline(IEncoder encoder, ILoadCell loadCell, IContactMonitor contactMonitor)
        {
            _encoder = encoder;
            _loadCell = loadCell;
            _contactMonitor = contactMonitor;
        }

        public async Task<ForceTravelCurve> AcquireAsync(int sampleCount, CancellationToken cancellationToken = default)
        {
            if (sampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be greater than zero.");

            var curve = new ForceTravelCurve();

            var acquisitionTimeout = TimeSpan.FromSeconds(30);

            var stopwatch = Stopwatch.StartNew();

            try
            {
                for (var index = 0; index < sampleCount; index++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    if (stopwatch.Elapsed >= acquisitionTimeout)
                    {
                        throw new TimeoutException(
                            $"Legacy acquisition timed out after " + 
                            $"{acquisitionTimeout.TotalSeconds:F0} seconds. " + 
                            $"{curve.Points.Count} of {sampleCount} samples " + "were acquired.");
                    }

                    _encoder.LatchPosition();

                    var encoderCount = _encoder.ReadCounts();

                    var force = _loadCell.ReadForce();

                    var contactState = _contactMonitor.State;

                    curve.Points.Add(new ForceTravelPoint
                        {
                            EncoderCount =
                                encoderCount,

                            Force =
                                force,

                            ContactState =
                                contactState
                        });

                    /*
                      Yield execution without imposing a fixed
                      acquisition period.
                     
                      Final sampling timing must be validated
                      against the physical Checker.
                     */
                    if ((index & 0x3F) == 0)
                        await Task.Yield();
                }

                return curve;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Legacy force-travel acquisition failed.", exception);
            }
        }
    }
}