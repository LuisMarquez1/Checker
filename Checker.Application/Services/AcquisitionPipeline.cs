using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class AcquisitionPipeline : IAcquisitionPipeline
    {
        private readonly IDataAcquisitionService _dataSource;
        private readonly IForceTravelRecorder _recorder;
        private readonly MachineConfiguration _configuration;

        public AcquisitionPipeline(IDataAcquisitionService service, IForceTravelRecorder recorder, MachineConfiguration configuration)
        {
            _dataSource = service;
            _recorder = recorder;
            _configuration = configuration;
        }

        public async Task<ForceTravelCurve> AcquireAsync(int sampleCount, CancellationToken cancellationToken = default)
        {
            _recorder.Start();

            var delayMilliseconds = 1000.0 / _configuration.SamplingRateHz;

            for (int i = 0; i < sampleCount; i++)
            {
                cancellationToken.ThrowIfCancellationRequested();

                var sample = _dataSource.Read();

                _recorder.Record(sample);

                await Task.Delay(TimeSpan.FromMilliseconds(delayMilliseconds), cancellationToken);
            }

            return _recorder.Stop();
        }
    }
}
