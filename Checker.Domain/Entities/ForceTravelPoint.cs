using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Enums;

namespace Checker.Domain.Entities
{
    public class ForceTravelPoint
    {
        public long EncoderCount { get; set; }
        public double Force { get; set; }
        public ContactState ContactState { get; set; }

    }
}
