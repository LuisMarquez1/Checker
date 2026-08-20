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
    public class TestResultRepository : ITestResultRepository
    {
        private readonly CheckerDbContext _db;

        public TestResultRepository(CheckerDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(TestExecutionResult result)
        {
            await _db.TestResults.AddAsync(result);

            await _db.SaveChangesAsync();
        }

        public async Task<TestExecutionResult> GetAsync(Guid id)
        {
            return await _db.TestResults
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
