using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Enums
{
    public enum TestSessionStatus
    {
        Idle,
        WaitingForTrigger,
        Acquiring,
        Completed,
        Cancelled,
        Error
    }
}
