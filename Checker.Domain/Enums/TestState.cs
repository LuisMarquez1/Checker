using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Enums
{
    public enum TestState
    {
        Idle,
        VerifyTestFixture,
        AcquireCurve,
        DetectEvents,
        AnalyzeCurve,
        CalculateMeasurements,
        EvaluateResults,
        Completed,
        Error
    }
}
