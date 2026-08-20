using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class Legacy5312Encoder : IEncoder
    {
        private const ushort EncoderBaseAddress = 0x300;
        private const ushort EncoderCommandAddress = 0x301;

        private const double CountsPerInch = 254000.0;

        private readonly IPortAccess _portAccess;
        private readonly IEncoderCalibrationStore _calibrationStore;
        private long? _encoderOffsetCounts;

        public Legacy5312Encoder(IPortAccess portAccess, IEncoderCalibrationStore calibrationStore)
        {
            _portAccess = portAccess;
            _calibrationStore = calibrationStore;
        }

        public long Position =>ReadCounts();

        public void Initialize()
        {
            var calibration = _calibrationStore.Load();

            _encoderOffsetCounts = calibration.EncoderOffsetCounts;

            _portAccess.WriteByte(EncoderBaseAddress, 0x01);

            Thread.Sleep(1);

            _portAccess.WriteByte(EncoderCommandAddress,0x35);

            Thread.Sleep(1);

            _portAccess.WriteByte(EncoderCommandAddress, 0x48);

            Thread.Sleep(1);

            _portAccess.WriteByte(EncoderCommandAddress, 0x80);

            Thread.Sleep(1);

            _portAccess.WriteByte(EncoderCommandAddress, 0xC3);

            Thread.Sleep(1);
        }

        public void Zero()
        {
            _portAccess.WriteByte(EncoderBaseAddress, 0x01);

            _portAccess.WriteByte(EncoderCommandAddress, 0x01);

            _portAccess.WriteByte(EncoderBaseAddress, 0x00);

            _portAccess.WriteByte(EncoderCommandAddress, 0x00);

            _portAccess.WriteByte(EncoderCommandAddress, 0x00);

            _portAccess.WriteByte(EncoderCommandAddress, 0x00);

            _portAccess.WriteByte(EncoderBaseAddress, 0x01);

            _portAccess.WriteByte(EncoderCommandAddress, 0x09);

            _portAccess.WriteByte(EncoderCommandAddress, 0x02);
        }

        public void LatchPosition()
        {
            _portAccess.WriteByte(EncoderBaseAddress, 0x01);

            _portAccess.WriteByte(EncoderCommandAddress, 0x03);
        }

        public long ReadCounts()
        {
            _portAccess.WriteByte(EncoderBaseAddress, 0x00);

            var low = _portAccess.ReadByte(EncoderCommandAddress);

            var middle = _portAccess.ReadByte(EncoderCommandAddress);

            var high = _portAccess.ReadByte(EncoderCommandAddress);

            var counts = low | (middle << 8) | (high << 16);

            return counts;
        }

        public double ReadPosition()
        {
            if (_encoderOffsetCounts is null)
                throw new InvalidOperationException("Encoder offset has not been set. Perform gage block calibration or load machine calibration before reading absolute position.");

            var rawCounts = ReadCounts();

            var relativeCounts = _encoderOffsetCounts.Value - rawCounts;

            return relativeCounts / CountsPerInch;
        }

        public void SetEncoderOffset(long encoderOffsetCounts)
        {
            _encoderOffsetCounts =encoderOffsetCounts;

            _calibrationStore.Save(new EncoderCalibration
            {
                    EncoderOffsetCounts =
                        encoderOffsetCounts,

                    UpdatedAt =
                        DateTime.UtcNow
            });
        }
    }
}