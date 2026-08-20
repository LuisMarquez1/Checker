using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Domain.Entities
{
    public class CoordinatedTestResult
    {
        public TestSession Session { get; set; } = new();
        public TestExecutionResult Result { get; set; } = new();
    }
}
