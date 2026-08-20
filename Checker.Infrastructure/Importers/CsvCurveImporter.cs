using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;

namespace Checker.Infrastructure.Importers
{
    public class CsvCurveImporter : ICurveImporter
    {
        public ForceTravelCurve Import(string filePath)
        {
            var curve = new ForceTravelCurve();

            var lines = File.ReadAllLines(filePath);

            foreach (var line in lines.Skip(1))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(',');

                curve.Points.Add(new ForceTravelPoint
                {
                    EncoderCount = long.Parse(parts[0]),
                    Force = double.Parse(parts[1]),
                    ContactState = Enum.Parse<ContactState>(parts[2])
                });
            }

            return curve;
        }
    }
}
