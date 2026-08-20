using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.UseCases;
using Checker.Domain.Entities;
using Checker.Domain.Enums;

namespace Checker.Application.Engines
{
    public class SwitchTestEngine
    {
        private readonly DetectEventsUseCase _detectEvents;
        private readonly AnalyzeCurveUseCase _analyzeCurve;
        private readonly CalculateMeasurementsUseCase _calculate;
        private readonly EvaluateResultUseCase _evaluate;

        public TestState CurrentState { get; set; } = TestState.Idle;

        public SwitchTestEngine(
            DetectEventsUseCase detectEvents,
            AnalyzeCurveUseCase analyzeCurve,
            CalculateMeasurementsUseCase calculate,
            EvaluateResultUseCase evaluate)
        {
            _detectEvents = detectEvents;
            _analyzeCurve = analyzeCurve;
            _calculate = calculate;
            _evaluate = evaluate;
        }

        public TestExecutionResult Execute(TestExecutionRequest request)
        {
            CurrentState = TestState.DetectEvents;

            var events = _detectEvents.Execute(request.Curve);

            CurrentState = TestState.AnalyzeCurve;

            var analysis = _analyzeCurve.Execute(request.Curve, events);

            CurrentState = TestState.CalculateMeasurements;

            var measurement = _calculate.Execute(new MeasurementCalculationInput
            {
                Curve = request.Curve,
                Events = events,

                OperatePointIndex = analysis.OperatePointIndex,
                ReleasePointIndex = analysis.ReleasePointIndex,
                FreePositionIndex = request.Configuration.FreePositionIndex,
                OverTravelIndex = request.Configuration.OverTravelIndex,

                BaselineForce = request.Configuration.BaselineForce,
                EncoderOffset = request.Configuration.EncoderOffset
            });

            CurrentState = TestState.EvaluateResults;

            var evaluation = _evaluate.Execute(measurement, request.Specification);

            CurrentState = TestState.Completed;

            return new TestExecutionResult
            {
                Events = events,
                Analysis = analysis,
                Measurement = measurement,
                Evaluation = evaluation
            };
        }

    }
}
