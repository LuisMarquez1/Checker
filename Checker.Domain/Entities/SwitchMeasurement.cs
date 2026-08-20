using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Domain.Entities
{
    public class SwitchMeasurement
    {
        public double OperatingForce { get; set; }
        public double ReleaseForce { get; set; }
        public double DifferentialForce { get; set; }
        public double FreePosition { get; set; }
        public double OperatePosition { get; set; }
        public double ReleasePosition { get; set; }
        public double DifferentialTravel { get; set; }
        public double PreTravel { get; set; }
        public double OverTravel { get; set; }
        public double ReturnTravel { get; set; }
        public double TotalTravel { get; set; }
        public double DeadBreakNO { get; set; }
        public double DeadBreakNC { get; set; }

    }
}
