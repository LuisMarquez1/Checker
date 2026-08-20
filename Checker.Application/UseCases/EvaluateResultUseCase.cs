using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Domain;
using Checker.Domain.Entities;

namespace Checker.Application.UseCases
{
    public class EvaluateResultUseCase
    {
        private readonly ISpecificationEvaluator _evaluator;

        public EvaluateResultUseCase(ISpecificationEvaluator evaluator)
        {
            _evaluator = evaluator;

        }

        public EvaluationResult Execute(SwitchMeasurement measurement, Specification specification)
        {
            return _evaluator.Evaluate(measurement, specification);
        }
    }
}
