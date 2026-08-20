using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Conditions
{
    public class SampleCountStopCondition : IStopCondition
    {
        private readonly int _sampleCount;

        public SampleCountStopCondition(int sampleCount)
        {
            _sampleCount = sampleCount;
        }
        public bool ShouldStop(ForceTravelCurve curve)
        {
            return curve.Points.Count >= _sampleCount;
        }
    }
}
