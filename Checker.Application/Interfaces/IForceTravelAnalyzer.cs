using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Entities;

namespace Checker.Application.Interfaces
{
    public interface IForceTravelAnalyzer
    {
        int LastMaxForcePoint(ForceTravelCurve curve, int beginIndex, int endIndex);
        int LastMinForcePoint(ForceTravelCurve curve, int beginIndex, int endIndex);
        double AverageForce(ForceTravelCurve curve, int beginIndex, int endIndex);
        int FindOperatePoint(ForceTravelCurve curve, int beginIndex, int endIndex);
        int FindReleasePoint(ForceTravelCurve curve, int beginIndex, int endIndex);
    }
}
