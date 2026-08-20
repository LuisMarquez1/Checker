using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class TestSession
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public DateTime StartedUtc { get; set; }
        public DateTime? CompletedUtc { get; set; }
        public TestSessionStatus Status { get; set; }
    }
}
