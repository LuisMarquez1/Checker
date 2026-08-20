using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class AcquisitionContext
    {
        public AcquisitionState State { get; set; } = AcquisitionState.Idle;
        public ForceTravelCurve Curve { get; set; } = new();
        public string? ErrorMessage { get; set; }
    }
}
