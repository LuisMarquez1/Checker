using Checker.Application.Interfaces;
using Checker.Persistance.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Persistance.Extensions
{
    public static class PersistenceDependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddScoped<IForceTravelCurveRepository, ForceTravelCurveRepository>();
            services.AddScoped<ITestSessionRepository, TestSessionRepository>();
            services.AddScoped<ITestResultRepository, TestResultRepository>();
            services.AddScoped<ISpecificationRepository, SpecificationRepository>();

            return services;
        }
    }
}
