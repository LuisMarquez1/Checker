using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.UseCases
{
    public class CalculateMeasurementsUseCase
    {
        private readonly IMeasurementCalculator _calculator;

        public CalculateMeasurementsUseCase(IMeasurementCalculator calculator)
        {
            _calculator = calculator;
        }

        public SwitchMeasurement Execute(MeasurementCalculationInput input)
        {
            return _calculator.Calculate(
                input.Curve,
                input.Events,
                input.OperatePointIndex,
                input.ReleasePointIndex,
                input.FreePositionIndex,
                input.OverTravelIndex,
                input.BaselineForce,
                input.EncoderOffset);
        }
    }
}
