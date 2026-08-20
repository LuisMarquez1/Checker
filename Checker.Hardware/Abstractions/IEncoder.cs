using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IEncoder
    {
        long Position { get; }
        void Initialize();
        void Zero();
        void LatchPosition();
        long ReadCounts();
        double ReadPosition();

        void SetEncoderOffset(long encoderOffsetCounts);

    }
}
