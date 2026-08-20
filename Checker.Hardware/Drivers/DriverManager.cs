using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Drivers
{
    public sealed class DriverManager
    {
        public IEncoder Encoder { get; set; }
        public ILoadCell LoadCell { get; set; }
        public IMotionController MotionController { get; set; }
        public IContactMonitor ContactMonitor { get; set; }
        public IFixtureController FixtureController { get; set; }

        public DriverManager(IEncoder encoder, ILoadCell loadCell, IMotionController motionController, IContactMonitor contactMonitor, IFixtureController fixtureController)
        {
            Encoder = encoder;
            LoadCell = loadCell;
            MotionController = motionController;
            ContactMonitor = contactMonitor;
            FixtureController = fixtureController;

        }
    }
}
