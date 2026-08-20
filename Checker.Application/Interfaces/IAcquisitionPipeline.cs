using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Interfaces
{
    public interface IAcquisitionPipeline
    {
        Task<ForceTravelCurve> AcquireAsync(int sampleCount, CancellationToken cancellationToken = default);
    }
}
