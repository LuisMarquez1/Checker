using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Enums
{
    public enum MeasurementType
    {
        OperatingForce,
        ReleaseForce,
        DifferentialForce,

        DifferentialTravel,
        OverTravel,

        DeadBreakNO,
        DeadBreakNC,

        OperatePosition,
        ReleasePosition,

        ReturnTravel,
        TotalTravel,
        PreTravel
    }
}
