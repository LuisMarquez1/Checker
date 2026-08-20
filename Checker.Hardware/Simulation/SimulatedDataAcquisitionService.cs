using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Simulation
{
    public class SimulatedDataAcquisitionService : IDataAcquisitionService
    {
        private int _counter;
        public AcquisitionSnapshot Read()
        {
            _counter++;

            ContactState state;

            var contact = new SimulatedSwitchGenerator();

            if (_counter < 25)
                state = ContactState.NC;
            else if (_counter < 50)
                state = ContactState.None;
            else if (_counter < 75)
                state = ContactState.NO;
            else if (_counter < 90)
                state = ContactState.None;
            else
                state = ContactState.NC;

            return new AcquisitionSnapshot
            {
                EncoderCount = _counter * 100,
                Force = _counter,
                ContactState = state
            };
        }
    }
}
