using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IEncoderCalibrationStore
    {
        EncoderCalibration Load();
        void Save(EncoderCalibration calibration);
    }
}
