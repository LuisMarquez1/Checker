using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class AcquisitionSnapshot
    {
        public long EncoderCount { get; set; }
        public double Force { get; set; }
        public ContactState ContactState{ get; set; }
    }
}
