using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Domain.Entities
{
    public class EvaluationResult
    {
        public bool Passed { get; set; }
        public List<MeasurementEvaluation> Measurements { get; set; } = new();
    }
}
