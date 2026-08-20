using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class MeasurementCalculationInput
    {
        public ForceTravelCurve Curve { get; set; } = null!;
        public ContactEvents Events { get; set; } = null!;
        public int OperatePointIndex { get; set; }
        public int ReleasePointIndex { get; set; }
        public int FreePositionIndex { get; set; }
        public int OverTravelIndex { get; set; }
        public double BaselineForce { get; set; }
        public double EncoderOffset { get; set; }
    }
}
