using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Enums
{
    [Flags]
    public enum ContactState
    {
        None = 0,
        NC = 1,
        NO = 2,
        Both = 3
    }
}
