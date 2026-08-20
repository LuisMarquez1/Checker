using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class TestExecutionResult
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        [NotMapped]
        public ContactEvents Events { get; set; } = new();
        [NotMapped]
        public CurveAnalysisResult Analysis { get; set; } = new();
        [NotMapped]
        public SwitchMeasurement Measurement { get; set; } = new();
        [NotMapped]
        public EvaluationResult Evaluation { get; set; } = new();
    }
}
