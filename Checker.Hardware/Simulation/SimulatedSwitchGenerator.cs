using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Checker.Domain.Enums;

namespace Checker.Hardware.Simulation
{
    public class SimulatedSwitchGenerator
    {
        public ForceTravelCurve Generate(SimulatedSwitchProfile profile)
        {
            var curve = new ForceTravelCurve();

            for (int i = 0; i < profile.TotalSamples; i++)
            {
                double force;

                if (i < profile.OperateIndex)
                    force = profile.PeakForce * i / profile.OperateIndex;
                else if (i < profile.ReleaseIndex)
                    force = profile.ReleaseForce + ((profile.ReleaseIndex - i) * 0.2);
                else
                    force = profile.ReleaseForce;

                var contact = ContactState.NC;

                if (i > profile.OperateIndex)
                    contact = ContactState.NO;

                if (i > profile.ReleaseIndex)
                    contact = ContactState.NC;

                curve.Points.Add(new ForceTravelPoint
                {
                    EncoderCount = i * 100,
                    Force = force,
                    ContactState = contact
                });
            }

            return curve;
        }
    }
}
