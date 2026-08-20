using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class SimulatedSwitchProfile
    {
        public int TotalSamples { get; set; } = 100;
        public double PeakForce { get; set; } = 20;
        public double ReleaseForce { get; set; } = 5;
        public int OperateIndex { get; set; } = 30;
        public int ReleaseIndex { get; set; } = 70;
    }
}
