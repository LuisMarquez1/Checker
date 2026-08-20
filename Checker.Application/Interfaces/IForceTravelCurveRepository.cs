using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Application.Interfaces
{
    public interface IForceTravelCurveRepository
    {
        Task AddAsync(StoredForceTravelCurve curve);
        Task<StoredForceTravelCurve> GetAsync(Guid id);
    }
}
