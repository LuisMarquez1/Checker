using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IContactMonitor
    {
        ContactState State { get; }
        bool NoContactClosed();
        bool NcContactClosed();
    }
}
