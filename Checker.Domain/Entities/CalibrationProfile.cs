using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class CalibrationProfile
    {
        public DateTime LastCalibrationDate { get; set; }
        public double ForceMultiplier { get; set; }
        public double TravelMultiplier { get; set; }
        public string CalibratedBy { get; set; } = string.Empty;
    }
}
