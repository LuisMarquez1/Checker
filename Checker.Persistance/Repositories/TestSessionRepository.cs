using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Microsoft.EntityFrameworkCore.Update;
using Checker.Domain.Entities;
using Checker.Persistance.Context;

namespace Checker.Persistance.Repositories
{
    public class TestSessionRepository : ITestSessionRepository
    {
        private readonly CheckerDbContext _db;

        public TestSessionRepository(CheckerDbContext db)
        {
            _db = db;
        }

        public async Task AddAsync(TestSession session)
        {
            await _db.TestSessions.AddAsync(session);

            await _db.SaveChangesAsync();
        }
    }
}
