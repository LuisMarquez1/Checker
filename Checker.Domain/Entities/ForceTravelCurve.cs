using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Checker.Domain.Entities
{
    public class ForceTravelCurve
    {
        [NotMapped]
        public List<ForceTravelPoint> Points { get; set; } = new();

        [NotMapped]
        public ContactEvents Events { get; set; } = new();
    }
}
