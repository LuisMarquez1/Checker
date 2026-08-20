using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Hardware.Abstractions;

namespace Checker.Hardware.Legacy
{
    public sealed class ConfigSpcCalibrationStore : IEncoderCalibrationStore
    {
        private const string EncoderOffsetKey = "EncoderOffsetCounts";

        private readonly string _filePath;

        public ConfigSpcCalibrationStore(string filePath)
        {
            _filePath = filePath;
        }
        public EncoderCalibration Load()
        {
            if (!File.Exists(_filePath))
            {
                return new EncoderCalibration
                {
                    EncoderOffsetCounts = 0
                };
            }

            var lines = File.ReadAllLines(_filePath);

            foreach (var line in lines)
            {
                if (!line.StartsWith(EncoderOffsetKey + "=" + StringComparison.OrdinalIgnoreCase))
                    continue;

                var valueText = line.Split('=')[1];

                if(long.TryParse(valueText, out var value))
                {
                    return new EncoderCalibration
                    {
                        EncoderOffsetCounts = value
                    };
                }
            }

            return new EncoderCalibration { EncoderOffsetCounts = 0 };
        }

        public void Save(EncoderCalibration calibration)
        {
            var directory = Path.GetDirectoryName(_filePath);

            if(!string.IsNullOrWhiteSpace(directory))
                Directory.CreateDirectory(directory);

            var lines = new List<string>();

            if (File.Exists(_filePath))
            {
                lines = File.ReadAllLines(_filePath)
                    .Where(x => !x.StartsWith(EncoderOffsetKey + "=", StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            lines.Add($"{EncoderOffsetKey}={calibration.EncoderOffsetCounts}");

            File.WriteAllLines(_filePath, lines);
        }
    }
}
