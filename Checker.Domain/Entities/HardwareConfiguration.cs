using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Enums;

namespace Checker.Domain.Entities
{
    public class HardwareConfiguration
    {
        public DriverType DriverType { get; set; }
        public int SamplingRateHz { get; set; } = 1000;
    }
}
