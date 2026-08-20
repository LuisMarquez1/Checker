using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Triggers
{
    public class ForceTriggerCondition : ITriggerCondition
    {
        private readonly double _threshold;

        public ForceTriggerCondition(double threshold)
        {
            _threshold = threshold;
        }

        public bool IsTriggered(AcquisitionSnapshot snapshot)
        {
            return snapshot.Force >= _threshold;
        }
    }
}
