using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IFixtureController
    {
        Task OpenAsync();
        Task CloseAsync();
    }
}
