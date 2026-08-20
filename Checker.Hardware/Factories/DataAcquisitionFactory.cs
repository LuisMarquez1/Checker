using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using Checker.Hardware.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Factories
{
    public class DataAcquisitionFactory : IDataAcquisitionFactory
    {
        private readonly HardwareConfiguration _configuration;

        public DataAcquisitionFactory(HardwareConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDataAcquisitionService Create()
        {
            return _configuration.DriverType switch
            {
                DriverType.Simulation => new SimulatedDataAcquisitionService(),

                _ => throw new NotImplementedException($"Driver {_configuration.DriverType} not implemented.")
            };
        }
    }
}
