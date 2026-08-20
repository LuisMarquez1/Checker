using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class MachineConfiguration
    {
        public string MachineName { get; set; } = string.Empty;
        public int SamplingRateHz { get; set; }
        public double EncoderResolution { get; set; }
        public double EncoderOffset { get; set; }
        public double BaselineForce { get; set; }
        public string ForceUnit { get; set; } = "g";
        public string TravelUnit { get; set; } = "mm";
    }
}
