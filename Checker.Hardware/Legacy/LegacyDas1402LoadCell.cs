using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public enum DasRange
    {
        Oz40,

        Oz20,

        Oz10,

        Oz5
    }

    public sealed class LoadCellCalibrationSnapshot
    {
        public int RawCounts { get; set; }

        public int BaselineCounts { get; set; }

        public double ForceOunces { get; set; }

        public double ForceGrams { get; set; }

        public DasRange Range { get; set; }

        public double Multiplier { get; set; }
    }

    public sealed class LegacyDas1402LoadCell : ILoadCell
    {
        private const ushort DasBaseAddress = 0x220;

        private const ushort DasAdLowData = 0;
        private const ushort DasAdHighData = 1;
        private const ushort DasMux = 2;
        private const ushort DasStatus = 8;
        private const ushort DasTrigger = 9;
        private const ushort DasGainRegister = 0x0B;

        private const byte EndOfConversionBit = 0x80;

        private const double MultiplierDas40Oz = 40.0 / 32767.0;
        private const double MultiplierDas20Oz = 20.0 / 32767.0;
        private const double MultiplierDas10Oz = 10.0 / 32767.0;
        private const double MultiplierDas5Oz = 5.0 / 32767.0;

        private const double GramsPerOunce = 28.34;

        private readonly IPortAccess _portAccess;

        private DasRange _range = DasRange.Oz40;

        private int _baselineCounts;

        public LegacyDas1402LoadCell(
            IPortAccess portAccess)
        {
            _portAccess = portAccess;
        }

        public double Force => ReadForce();

        public DasRange CurrentRange => _range;

        public double CurrentMultiplier => GetMultiplier();

        public int BaselineCounts => _baselineCounts;

        public void Initialize()
        {
            _portAccess.WriteByte(Address(DasTrigger), 0);

            SetRange(DasRange.Oz40);

            _portAccess.WriteByte(Address(DasMux), 0);

            _portAccess.WriteByte(Address(DasStatus), 1);

            Thread.Sleep(5);

            StartConversion();
        }

        public void SetRange(DasRange range)
        {
            _range = range;

            var gainValue = range switch
                {
                    DasRange.Oz40 => (byte)0,
                    DasRange.Oz20 => (byte)1,
                    DasRange.Oz10 => (byte)2,
                    DasRange.Oz5 => (byte)3,
                    _ => (byte)0
                };

            _portAccess.WriteByte(Address(DasGainRegister), gainValue);
        }

        public DasRange ToggleRange()
        {
            var nextRange = _range switch
                {
                    DasRange.Oz40 => DasRange.Oz20,
                    DasRange.Oz20 => DasRange.Oz10,
                    DasRange.Oz10 => DasRange.Oz5,
                    DasRange.Oz5 => DasRange.Oz40,
                    _ => DasRange.Oz40
                };

            SetRange(nextRange);

            RetakeBaseline();

            return nextRange;
        }

        public int RetakeBaseline()
        {
            _baselineCounts = ReadAverageRaw(16);

            return _baselineCounts;
        }

        public void SetBaseline(int baselineCounts)
        {
            _baselineCounts = baselineCounts;
        }

        public int ReadRaw()
        {
            WaitForConversion();

            var high = _portAccess.ReadByte(Address(DasAdHighData));

            var low = _portAccess.ReadByte(Address(DasAdLowData));

            var value = (high << 8) | low;

            StartConversion();

            return value;
        }

        public int ReadAverageRaw(int sampleCount)
        {
            if (sampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be greater than zero.");

            long sum = 0;

            StartConversion();

            for (var index = 0; index < sampleCount; index++)
                sum += ReadRaw();

            return (int)(sum / sampleCount);
        }

        public double ReadForce()
        {
            var rawCounts = ReadRaw();

            return ConvertCountsToOunces(rawCounts - _baselineCounts);
        }

        public double ReadAverageForce(int sampleCount)
        {
            var rawCounts = ReadAverageRaw(sampleCount);

            return ConvertCountsToOunces(rawCounts - _baselineCounts);
        }

        public LoadCellCalibrationSnapshot ReadCalibrationSnapshot()
        {
            var rawCounts = ReadAverageRaw(16);

            var forceOunces = ConvertCountsToOunces(rawCounts - _baselineCounts);

            return new LoadCellCalibrationSnapshot
            {
                RawCounts =rawCounts,
                BaselineCounts = _baselineCounts,
                ForceOunces = forceOunces,
                ForceGrams = forceOunces * GramsPerOunce,
                Range = _range,
                Multiplier = GetMultiplier()
            };
        }

        private void StartConversion()
        {
            _portAccess.WriteByte(Address(DasMux), 0);

            _portAccess.WriteByte(DasBaseAddress, 1);
        }

        private void WaitForConversion()
        {
            var startedAt = DateTime.UtcNow;

            while (true)
            {
                var status = _portAccess.ReadByte(Address(DasStatus));

                if ((status & EndOfConversionBit) == 0)
                    return;

                if ((DateTime.UtcNow - startedAt).TotalMilliseconds > 2000)
                    throw new TimeoutException("DAS1402 A/D conversion timed out.");

                Thread.Sleep(1);
            }
        }

        private double ConvertCountsToOunces(int relativeCounts)
        {
            return relativeCounts * GetMultiplier();
        }

        private double GetMultiplier()
        {
            return _range switch
            {
                DasRange.Oz40 => MultiplierDas40Oz,
                DasRange.Oz20 => MultiplierDas20Oz,
                DasRange.Oz10 => MultiplierDas10Oz,
                DasRange.Oz5 => MultiplierDas5Oz,
                _ => MultiplierDas40Oz
            };
        }

        private static ushort Address(ushort offset)
        {
            return (ushort)(DasBaseAddress + offset);
        }
    }
}