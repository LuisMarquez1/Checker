using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Legacy
{
    public sealed class LacSerialOptions
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public int ReadTimeoutMiliseconds { get; set; } = 2000;
        public int WriteTimeoutMiliseconds { get; set; } = 2000;
        public long SpeedMultiplier { get; set; }
        public int Torque { get; set; }
        public int ProportionalGain { get; set; }
        public int IntegralGain { get; set; }
        public int DerivativeGain { get; set; }
        public int IntegralLimit { get; set; }
        public int CurrentGain { get; set; }
        public long FastVelocity { get; set; }
        public long  MediumVelocity { get; set; }
        public long TestVelocity { get; set; }
        public long Acceleration { get; set; }
        public int OverTravelTorqueAdjust { get; set; }
    }
}
