using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class ContactEvents
    {
        public int? FTLoseNC1 { get; set; }
        public int? FTMakeNO1 { get; set; }

        public int? FTLoseNO1 { get; set; }
        public int? FTMakeNC1 { get; set; }

        public int? FTLoseNC2 { get; set; }
        public int? FTMakeNO2 { get; set; }

        public int? FTTurnaround1 { get; set; }
        public int? FTTurnaround2 { get; set; }
        public int? FTTurnaround3 { get; set; }
    }
}
