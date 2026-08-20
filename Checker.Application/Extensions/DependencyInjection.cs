using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Calculators;
using Checker.Application.Engines;
using Checker.Application.Interfaces;
using Checker.Application.Services;
using Checker.Application.UseCases;
using Checker.Application.Triggers;
using Checker.Domain.Entities;
using Checker.Application.Conditions;
using Microsoft.Extensions.DependencyInjection;

namespace Checker.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IForceTravelAnalyzer, ForceTravelAnalyzer>();
            services.AddScoped<IMeasurementCalculator, MeasurementCalculator>();
            services.AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();
            services.AddScoped<IForceTravelRecorder, ForceTravelRecorder>();
            services.AddScoped<DetectEventsUseCase>();
            services.AddScoped<AnalyzeCurveUseCase>();
            services.AddScoped<CalculateMeasurementsUseCase>();
            services.AddScoped<EvaluateResultUseCase>();
            services.AddScoped<ReplayCurveUseCase>();
            services.AddScoped<SwitchTestEngine>();
            services.AddScoped<OnlineTestEngine>();
            services.AddScoped<AcquireCurveUseCase>();
            services.AddScoped<IAcquisitionPipeline, AcquisitionPipeline>();
            //services.AddScoped<IDataAcquisitionService, SimulatedDataAcquisitionService>();
            services.AddScoped<IForceTravelRecorder, ForceTravelRecorder>();
            services.AddScoped<MachineConfiguration>();
            services.AddScoped<IAcquisitionStateMachine, AcquisitionStateMachine>();
            services.AddScoped<TestSessionManager>();
            services.AddScoped<TestCoordinator>();
            services.AddScoped<ForceTravelCurveSerializer>();
            services.AddScoped<PersistanceCoordinator>();
            services.AddScoped<ITriggerCondition>(_ => new ForceTriggerCondition(0));
            services.AddScoped<IStopCondition>(_ => new SampleCountStopCondition(100));


            services.AddScoped<ITriggerCondition>(_ => new ForceTriggerCondition(0));

            return services;
        }
    }
}
