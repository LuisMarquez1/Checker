using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace Checker.Persistance.Repositories
{
    public class ForceTravelCurveRepository : IForceTravelCurveRepository
    {
        private readonly CheckerDbContext _db;

        public ForceTravelCurveRepository(CheckerDbContext db)
        {
                _db = db;
        }
        public async Task AddAsync(StoredForceTravelCurve curve)
        {
            await _db.ForceTravelCurve.AddAsync(curve);

            await _db.SaveChangesAsync();
        }

        public async Task<StoredForceTravelCurve> GetAsync(Guid id)
        {
            return await _db.ForceTravelCurve
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
