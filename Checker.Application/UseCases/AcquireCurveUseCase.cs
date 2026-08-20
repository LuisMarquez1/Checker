using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Checker.Application.Interfaces;

namespace Checker.Application.UseCases
{
    public class AcquireCurveUseCase
    {
        private readonly IAcquisitionPipeline _pipeline;

        public AcquireCurveUseCase(IAcquisitionPipeline pipeline)
        {
            _pipeline = pipeline;
        }

        public Task<ForceTravelCurve> ExecuteAsync (int samples, CancellationToken cancellationToken)
        {
            return _pipeline.AcquireAsync(samples, cancellationToken);
        }
    }
}
