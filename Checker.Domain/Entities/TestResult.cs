using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Domain.Entities
{
    public class TestResult
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public DateTime TimestampUtc { get; set; }
        public bool Passed { get; set; }

        [NotMapped]
        public SwitchMeasurement Measurement { get; set; } = new();

        [NotMapped]
        public EvaluationResult Evaluation { get; set; } = new();
    }
}
