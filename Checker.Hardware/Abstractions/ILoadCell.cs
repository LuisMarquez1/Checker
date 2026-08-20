using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface ILoadCell
    {
        double Force { get; }
        int ReadRaw();
        int ReadAverageRaw(int sampleCount);
        double ReadForce();
        double ReadAverageForce(int sampleCount);
        void SetBaseline(int baselineCounts);
    }
}
