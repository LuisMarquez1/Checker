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
    public class SpecificationRepository : ISpecificationRepository
    {
        private readonly CheckerDbContext _db;

        public SpecificationRepository(CheckerDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(Specification specification)
        {
            await _db.Specifications.AddAsync(specification);

            await _db.SaveChangesAsync();
        }

        public async Task<List<Specification>> GetAllAsync()
        {
            return await _db.Specifications
                .Include(x => x.Limits)
                .ToListAsync();
        }

        public async Task<Specification> GetByIdAsync(Guid id)
        {
            return await _db.Specifications
                .Include(x => x.Limits)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Specification> GetByPartNumberAsync(string partNumber)
        {
            return await _db.Specifications
                .Include(x => x.Limits)
                .FirstOrDefaultAsync(x => x.PartNumber == partNumber);
        }

        public async Task updateAsync(Specification specification)
        {
            _db.Specifications.Update(specification);

            await _db.SaveChangesAsync();
        }
    }
}
