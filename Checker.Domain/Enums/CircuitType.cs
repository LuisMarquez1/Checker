using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Enums
{
    [Flags]
    public enum CircuitType
    {
        SPDT = 0,
        NOOnly = 1,
        NCOnly = 2,
        DblSnap = 3
    }
}
