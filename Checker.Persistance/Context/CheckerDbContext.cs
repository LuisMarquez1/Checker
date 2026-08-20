using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Checker.Persistance.Context
{
    public class CheckerDbContext : DbContext
    {
        public CheckerDbContext(DbContextOptions<CheckerDbContext> options) : base(options)
        {

        }

        public DbSet<Specification> Specifications => Set<Specification>();
        public DbSet<TestSession> TestSessions => Set<TestSession>();
        public DbSet<TestExecutionResult> TestResults => Set<TestExecutionResult>();
        public DbSet<StoredForceTravelCurve> ForceTravelCurve => Set<StoredForceTravelCurve>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckerDbContext).Assembly);
        }
    }
}
