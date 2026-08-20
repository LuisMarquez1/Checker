using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Checker.Persistance.Context
{
    public class CheckerDbContextFactory : IDesignTimeDbContextFactory<CheckerDbContext>
    {
        public CheckerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CheckerDbContext>();

            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=CheckerDb;Username=postgres;Password=123");

            return new CheckerDbContext(optionsBuilder.Options);
        }
    }
}
