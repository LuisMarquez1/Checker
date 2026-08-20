using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Application.Interfaces
{
    public interface ISpecificationEvaluator
    {
        EvaluationResult Evaluate(SwitchMeasurement measurement, Specification specification);
    }
}
