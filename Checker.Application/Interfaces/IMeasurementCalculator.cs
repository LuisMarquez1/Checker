using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;

namespace Checker.Application.Interfaces
{
    public interface IMeasurementCalculator
    {
        SwitchMeasurement Calculate(
            ForceTravelCurve curve,
            ContactEvents events,
            int operatePointIndex, 
            int releasePointIndex,
            int freePositionIndex,
            int overTravelIndex,
            double baselineForce, 
            double encoderOffset);
    }
}
