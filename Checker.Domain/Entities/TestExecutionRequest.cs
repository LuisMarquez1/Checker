using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class TestExecutionRequest
    {
        public ForceTravelCurve Curve { get; set; } = new();
        public Specification Specification { get; set; } = new();
        public TestConfiguration Configuration { get; set; } = new();
    }
}
