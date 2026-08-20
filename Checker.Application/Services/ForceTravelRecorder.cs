using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class ForceTravelRecorder : IForceTravelRecorder
    {
        private readonly ForceTravelCurve _curve = new();

        public ForceTravelCurve CurrentCure
        {
            get {  return _curve; }
        }

        public void Record(AcquisitionSnapshot snapshot)
        {
            _curve.Points.Add(new ForceTravelPoint
            {
                EncoderCount = snapshot.EncoderCount,
                Force = snapshot.Force,
                ContactState = snapshot.ContactState,
            });
        }

        public void Start()
        {
            _curve.Points.Clear();
        }

        public ForceTravelCurve Stop()
        {
            return _curve;
        }

        
    }
}
