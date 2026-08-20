using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Calculators
{
    public class MeasurementCalculator : IMeasurementCalculator
    {
        //  Position helper
        private static double AbsolutePosition(double encoderOffset, long encoderCount)
        {
            return Math.Abs(encoderOffset - encoderCount);
        }

        public SwitchMeasurement Calculate(
            ForceTravelCurve curve,
            ContactEvents events,
            int operatePointIndex,
            int releasePointIndex,
            int freePositionIndex,
            int overTravelIndex,
            double baselineForce, 
            double encoderOffset)
        {
            var operatePoint = curve.Points[operatePointIndex];
            var releasePoint = curve.Points[releasePointIndex];
            var freePoint = curve.Points[freePositionIndex];
            var overTravelPoint = curve.Points[overTravelIndex];
            var overTravelPosition = AbsolutePosition(encoderOffset, overTravelPoint.EncoderCount);

            var result = new SwitchMeasurement();

            result.OperatingForce = Math.Abs(operatePoint.Force - baselineForce);
            result.ReleaseForce = Math.Abs(releasePoint.Force - baselineForce);
            result.DifferentialForce = result.OperatingForce - result.ReleaseForce;

            result.OperatePosition = AbsolutePosition(encoderOffset, operatePoint.EncoderCount);
            result.ReleasePosition = AbsolutePosition(encoderOffset, releasePoint.EncoderCount);
            result.DifferentialTravel = result.OperatePosition - result.ReleasePosition;

            if(result.DifferentialTravel <0)
                result.DifferentialTravel = 0;

            result.FreePosition = AbsolutePosition(encoderOffset, freePoint.EncoderCount);
            result.PreTravel = result.FreePosition - result.OperatePosition;
            result.ReturnTravel = result.FreePosition - result.ReleasePosition;
            result.OverTravel = result.OperatePosition - overTravelPosition;
            result.TotalTravel = result.FreePosition - overTravelPosition;

            // Calculate DBNO
            if (events.FTLoseNO1.HasValue)
            {
                var loseNoPosition = AbsolutePosition(encoderOffset, curve.Points[events.FTLoseNO1.Value].EncoderCount);

                result.DeadBreakNO = result.ReleasePosition - loseNoPosition;

                if(result.DeadBreakNO < 0)
                    result.DeadBreakNO = 0;
            }

            // Calculate DBNC
            if (events.FTLoseNC2.HasValue)
            {
                var loseNcPosition = AbsolutePosition(encoderOffset, curve.Points[events.FTLoseNC2.Value].EncoderCount);
                result.DeadBreakNC = loseNcPosition - result.OperatePosition;

                if(result.DeadBreakNC < 0)
                    result.DeadBreakNC = 0;
            }

            return result;
        }
    }
}
