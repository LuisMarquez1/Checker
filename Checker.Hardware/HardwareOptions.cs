using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware
{
    public sealed class HardwareOptions
    {
        public DriverMode DriverMode { get; set; } = DriverMode.Simulation;
    }
}
