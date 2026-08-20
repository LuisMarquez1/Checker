using Checker.Domain.Enums;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class LegacyPcdio120Controller :
        IOperatorControls,
        IContactMonitor,
        IFixtureController
    {
        private readonly IPortAccess _portAccess;

        private const ushort ControlRegister2 = 0x323;

        private const ushort HeadInputPort = 0x320;
        private const ushort LacOutputPort = 0x321;
        private const ushort HeadOutputPort = 0x322;

        private const byte StartSwitchBit = 0x01;
        private const byte StopSwitchBit = 0x02;
        private const byte NoContactBit = 0x08;
        private const byte NcContactBit = 0x10;
        private const byte LacCommandAckBit = 0x40;
        private const byte LacInitializedBit = 0x80;

        private const byte MotorStopBit = 0x01;
        private const byte StopLacInterruptBit = 0x02;
        private const byte StartLacInterruptBit = 0x04;

        private const byte SsrNestAvBit = 0x02;
        private const byte OtRelayBit = 0x04;

        private const byte AllSsrsOff = 0xFF;

        public LegacyPcdio120Controller(
            IPortAccess portAccess)
        {
            _portAccess = portAccess;
        }

        public void Initialize()
        {
            _portAccess.WriteByte(
                ControlRegister2,
                0x90);

            _portAccess.WriteByte(
                HeadOutputPort,
                AllSsrsOff);
        }

        public bool StartPressed()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & StartSwitchBit) == 0;
        }

        public bool StopPressed()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & StopSwitchBit) == 0;
        }

        public ContactState State
        {
            get
            {
                var noClosed =
                    NoContactClosed();

                var ncClosed =
                    NcContactClosed();

                if (noClosed && ncClosed)
                {
                    return ContactState.Both;
                }

                if (noClosed)
                {
                    return ContactState.NO;
                }

                if (ncClosed)
                {
                    return ContactState.NC;
                }

                return ContactState.None;
            }
        }

        public bool NoContactClosed()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & NoContactBit) == NoContactBit;
        }

        public bool NcContactClosed()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & NcContactBit) == NcContactBit;
        }

        public bool LacAcknowledged()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & LacCommandAckBit) == LacCommandAckBit;
        }

        public bool LacInitialized()
        {
            var value =
                _portAccess.ReadByte(
                    HeadInputPort);

            return (value & LacInitializedBit) == LacInitializedBit;
        }

        public Task CloseAsync()
        {
            FireNest();

            return Task.CompletedTask;
        }

        public Task OpenAsync()
        {
            LowerNest();

            return Task.CompletedTask;
        }

        public void FireNest()
        {
            var current =
                _portAccess.ReadByte(
                    HeadOutputPort);

            var next =
                (byte)(current & ~SsrNestAvBit);

            _portAccess.WriteByte(
                HeadOutputPort,
                next);
        }

        public void LowerNest()
        {
            var current =
                _portAccess.ReadByte(
                    HeadOutputPort);

            var next =
                (byte)(current | SsrNestAvBit);

            _portAccess.WriteByte(
                HeadOutputPort,
                next);
        }

        public void ToggleNest()
        {
            var current =
                _portAccess.ReadByte(
                    HeadOutputPort);

            var next =
                (byte)(current ^ SsrNestAvBit);

            _portAccess.WriteByte(
                HeadOutputPort,
                next);
        }

        public void TurnOtRelayOn()
        {
            var current =
                _portAccess.ReadByte(
                    HeadOutputPort);

            var next =
                (byte)(current & ~OtRelayBit);

            _portAccess.WriteByte(
                HeadOutputPort,
                next);
        }

        public void TurnOtRelayOff()
        {
            var current =
                _portAccess.ReadByte(
                    HeadOutputPort);

            var next =
                (byte)(current | OtRelayBit);

            _portAccess.WriteByte(
                HeadOutputPort,
                next);
        }

        public void TriggerMotorStop()
        {
            PulseLacOutput(
                MotorStopBit,
                10);
        }

        public void TriggerStartLacInterrupt()
        {
            PulseLacOutput(
                StartLacInterruptBit,
                10);
        }

        public void TriggerStopLacInterrupt()
        {
            PulseLacOutput(
                StopLacInterruptBit,
                10);
        }

        public void WriteLacCommand(
            byte command)
        {
            _portAccess.WriteByte(
                LacOutputPort,
                command);
        }

        public void ClearLacCommand()
        {
            _portAccess.WriteByte(
                LacOutputPort,
                0);
        }

        private void PulseLacOutput(
            byte signal,
            int milliseconds)
        {
            _portAccess.WriteByte(
                LacOutputPort,
                signal);

            Thread.Sleep(
                milliseconds);

            _portAccess.WriteByte(
                LacOutputPort,
                0);
        }
    }
}