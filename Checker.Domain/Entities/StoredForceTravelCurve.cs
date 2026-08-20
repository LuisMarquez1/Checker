using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class StoredForceTravelCurve
    {
        public Guid Id { get; set; }
        public Guid SessionId { get; set; }
        public string CurveJson { get; set; } = string.Empty;
    }
}
