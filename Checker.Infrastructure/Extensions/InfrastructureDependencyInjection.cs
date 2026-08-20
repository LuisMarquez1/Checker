using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Infrastructure.Importers;
using Microsoft.Extensions.DependencyInjection;

namespace Checker.Infrastructure.Extensions
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICurveImporter, CsvCurveImporter>();

            return services;
        }
    }
}
