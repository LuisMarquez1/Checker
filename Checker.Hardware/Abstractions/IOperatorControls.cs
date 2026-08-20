using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Abstractions
{
    public interface IOperatorControls
    {
        bool StartPressed();
        bool StopPressed();
    }
}
