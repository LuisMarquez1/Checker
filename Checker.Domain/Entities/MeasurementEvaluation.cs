using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Enums;

namespace Checker.Domain.Entities
{
    public class MeasurementEvaluation
    {
        public MeasurementType MeasurementType { get; set; }
        public double Value { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
        public bool Passed { get; set; }
    }
}
