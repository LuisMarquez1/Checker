using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public class LegacyPortAccess : IPortAccess
    {
        public byte ReadByte(ushort address)
        {
            try
            {
                var value = NativePortAccess.Inp32(unchecked((short)address));

                return (byte)(value & 0xFF);
            }
            catch (DllNotFoundException ex)
            {
                throw new InvalidOperationException("inputx64.dll was not found. Copy inputx64.dll to the application output folder", ex);
            }
        }

        public void WriteByte(ushort address, byte value)
        {
            try
            {
                NativePortAccess.Out32(unchecked((short)address), value);

            }
            catch (DllNotFoundException ex)
            {
                throw new InvalidOperationException("inpoutx64.dll was not found. Copy inpoutx64.dll to the application output folder.", ex);
            }
        }

        public static class NativePortAccess
        {
            [DllImport("inpoutx64.dll", EntryPoint = "Inp32")]
            public static extern short Inp32(short address);

            [DllImport("inpoutx64.dll", EntryPoint = "Out32")]
            public static extern void Out32(short portAddress, short data);
        }
    }
}
