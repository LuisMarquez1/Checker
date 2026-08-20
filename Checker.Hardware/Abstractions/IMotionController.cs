using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IMotionController
    {
        Task MoveUpAsync(double speed);
        Task MoveDownAsync(double speed);
        Task RaiseHeadAsync(double distance);
        Task LowerHeadAsync(double distance);
        Task StopAsync();
    }
}
