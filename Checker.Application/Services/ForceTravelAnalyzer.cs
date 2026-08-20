using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;
using Checker.Application.Interfaces;

namespace Checker.Application.Services
{
    public class ForceTravelAnalyzer : IForceTravelAnalyzer
    {
        public double AverageForce(ForceTravelCurve curve, int beginIndex, int endIndex)
        {
            if (beginIndex == endIndex)
                return curve.Points[beginIndex].Force;

            var points = curve.Points
                .Skip(beginIndex)
                .Take(endIndex);

            return points.Average(p => p.Force);
        }

        public int LastMaxForcePoint(ForceTravelCurve curve, int beginIndex, int endIndex)
        {
            double maxForce = double.MinValue;
            int maxIndex = beginIndex;

            for (int i = beginIndex; i <= endIndex; i++)
            {
                var force = curve.Points[i].Force;

                if(force >= maxForce)
                {
                    maxForce = force;
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        public int LastMinForcePoint(ForceTravelCurve curve, int beginIndex, int endIndex)
        {
            double minForce = double.MaxValue;
            int minIndex = beginIndex;

            for(int i = beginIndex; i <= endIndex; i++)
            {
                var force = curve.Points[i].Force;

                if(force <= minForce)
                {
                    minForce = force;
                    minIndex = i;
                }
            }

            return minIndex;
        }

        public int FindOperatePoint(ForceTravelCurve curve, int beginIndex, int endIndex)
        {
            const int groupSize = 5;

            double maxGroupForce = AverageForce(curve, beginIndex, beginIndex + (groupSize - 1));

            int maxGroup = beginIndex;

            int index = beginIndex + groupSize;

            while( (index + groupSize - 1) < endIndex)
            {
                double currentGroupForce = AverageForce(curve, index, index + (groupSize - 1));

                if(currentGroupForce >= maxGroupForce)
                {
                    maxGroupForce = currentGroupForce;
                    maxGroup = index;
                }

                index += groupSize;
            }

            int searchEnd = Math.Min(maxGroup + ((2 * groupSize) - 1), curve.Points.Count - 1);

            return LastMaxForcePoint(curve, maxGroup, searchEnd);
        }

        public int FindReleasePoint(ForceTravelCurve curve, int beginIndex, int endIndex)
        {
            const int groupSize = 5;

            double minGroupForce = AverageForce(curve, beginIndex, beginIndex + (groupSize - 1));

            int minGroup = beginIndex;

            int index = beginIndex;

            while((index + groupSize - 1) < endIndex)
            {
                double currentGroupForce = AverageForce(curve, index, index + (groupSize - 1));

                if(currentGroupForce <= minGroupForce)
                {
                    minGroupForce = currentGroupForce;
                    minGroup = index;
                }

                index += groupSize;
            }

            int searchEnd = Math.Min(minGroup + (2 * groupSize) - 1, curve.Points.Count - 1);

            return LastMinForcePoint(curve, minGroup, searchEnd);
        }
    }
}
