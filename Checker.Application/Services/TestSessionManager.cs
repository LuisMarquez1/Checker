using Checker.Domain.Entities;
using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class TestSessionManager
    {
        public TestSession Start()
        {
            return new TestSession
            {
                Id = Guid.NewGuid(),
                StartedUtc = DateTime.UtcNow,
                Status = TestSessionStatus.WaitingForTrigger
            };
        }

        public void Complete(TestSession session)
        {
            session.CompletedUtc = DateTime.UtcNow;

            session.Status = TestSessionStatus.Completed;
        }
    }
}
