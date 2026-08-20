using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Hardware.Abstractions
{
    public interface IPortAccess
    {
        byte ReadByte(ushort address);
        void WriteByte(ushort address, byte value);
    }
}
