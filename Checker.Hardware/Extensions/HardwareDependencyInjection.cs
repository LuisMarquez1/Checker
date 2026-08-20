using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using Checker.Hardware.Abstractions;
using Checker.Hardware.Legacy;
using Microsoft.Extensions.DependencyInjection;

namespace Checker.Hardware.Extensions
{
    public static class HardwareDependencyInjection
    {
        public static IServiceCollection AddHardware(
            this IServiceCollection services)
        {
            services.AddSingleton(new HardwareConfiguration
                {
                    DriverType = DriverType.Legacy,
                    SamplingRateHz = 1000
                });

            services.AddSingleton<IPortAccess, LegacyPortAccess>();

            services.AddSingleton<IEncoderCalibrationStore>(_ => new ConfigSpcCalibrationStore(@"C:\Checker\Config.spc"));

            services.AddSingleton<LegacyPcdio120Controller>();

            services.AddSingleton<IOperatorControls>(provider => provider.GetRequiredService<LegacyPcdio120Controller>());

            services.AddSingleton<IContactMonitor>(provider => provider.GetRequiredService<LegacyPcdio120Controller>());

            services.AddSingleton<IFixtureController>(provider => provider.GetRequiredService<LegacyPcdio120Controller>());

            services.AddSingleton(new LacSerialOptions
                {
                    PortName = "COM1",
                    BaudRate = 9600,
                    ReadTimeoutMiliseconds = 2000,
                    WriteTimeoutMiliseconds = 2000,

                    SpeedMultiplier = 25400000,

                    Torque = 20000,
                    ProportionalGain = 25,
                    IntegralGain = 75,
                    DerivativeGain = 250,
                    IntegralLimit = 1,
                    CurrentGain = 1,

                    FastVelocity = 20000000,
                    MediumVelocity = 80000,
                    TestVelocity = 508000,
                    Acceleration = 10000,
                    OverTravelTorqueAdjust = 700
                });

            services.AddSingleton<LegacyLacController>();

            services.AddSingleton<IMotionController>(provider =>provider.GetRequiredService<LegacyLacController>());

            services.AddSingleton<Legacy5312Encoder>();

            services.AddSingleton<IEncoder>(provider => provider.GetRequiredService<Legacy5312Encoder>());

            services.AddSingleton<LegacyDas1402LoadCell>();

            services.AddSingleton<ILoadCell>(provider => provider.GetRequiredService<LegacyDas1402LoadCell>());

            services.AddSingleton<LegacyCalibrationController>();

            services.AddSingleton<LegacyHardwareInitializer>();

            services.AddSingleton<LegacyHardwareDiagnostics>();

            services.AddSingleton<IAcquisitionPipeline, LegacyAcquisitionPipeline>();

            return services;
        }
    }
}