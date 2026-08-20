using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public sealed class EncoderCalibration
    {
        public long EncoderOffsetCounts { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
