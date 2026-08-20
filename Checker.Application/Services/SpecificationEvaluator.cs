using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class SpecificationEvaluator : ISpecificationEvaluator
    {
        // Extraction Method
        private static double GetMeasurementValue(SwitchMeasurement measurement, MeasurementType type)
        {
            return type switch
            {
                MeasurementType.OperatingForce => measurement.OperatingForce,
                MeasurementType.ReleaseForce => measurement.ReleaseForce,
                MeasurementType.DifferentialForce => measurement.DifferentialForce,

                MeasurementType.OperatePosition => measurement.OperatePosition,
                MeasurementType.ReleasePosition => measurement.ReleasePosition,
                MeasurementType.DifferentialTravel => measurement.DifferentialTravel,

                MeasurementType.ReturnTravel => measurement.ReturnTravel,
                MeasurementType.PreTravel => measurement.PreTravel,
                MeasurementType.TotalTravel => measurement.TotalTravel,
                MeasurementType.OverTravel => measurement.OverTravel,

                MeasurementType.DeadBreakNC => measurement.DeadBreakNC,
                MeasurementType.DeadBreakNO => measurement.DeadBreakNO,

                _ => throw new NotSupportedException()
            };
        }
        public EvaluationResult Evaluate(SwitchMeasurement measurement, Specification specification)
        {
            var result = new EvaluationResult
            {
                Passed = true
            };

            foreach (var limit in specification.Limits)
            {
                var value = GetMeasurementValue(measurement, limit.MeasurementType);
                bool passed = true;

                if(limit.Minimum.HasValue)
                    passed &= value >= limit.Minimum.Value;

                if(limit.Maximum.HasValue)
                    passed &= value <= limit.Maximum.Value;

                var evaluation = new MeasurementEvaluation
                {
                    MeasurementType = limit.MeasurementType,
                    Value = value,
                    Minimum = limit.Minimum,
                    Maximum = limit.Maximum,
                    Passed = passed
                };

                result.Measurements.Add(evaluation);

                if (!passed)
                    result.Passed = false;
            }

            return result;
        }
    }
}
