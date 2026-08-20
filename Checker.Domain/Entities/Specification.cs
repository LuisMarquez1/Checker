using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Enums;

namespace Checker.Domain.Entities
{
    public class Specification
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;
        public CircuitType CircuitType { get; set; }
        public ICollection<MeasurementLimit> Limits { get; set; } = new List<MeasurementLimit>();
    }
}
