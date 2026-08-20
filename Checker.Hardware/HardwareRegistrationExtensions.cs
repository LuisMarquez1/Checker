using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Checker.Hardware
{
    public static class HardwareRegistrationExtensions
    {
        public static IServiceCollection AddCheckerHardware(this IServiceCollection services, HardwareOptions options)
        {
            switch (options.DriverMode)
            {
                case DriverMode.Legacy:
                    // Here we implement all Legacy Checker hardware
                    break;

                case DriverMode.Simulation:
                    break;

                default:
                    break;
            }

            return services;
        }
    }
}
