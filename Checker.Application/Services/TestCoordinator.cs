using Checker.Application.Engines;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class TestCoordinator
    {
        private readonly TestSessionManager _sessionManager;
        private readonly IAcquisitionStateMachine _acquisition;
        private readonly SwitchTestEngine _switchTestEngine;
        private readonly PersistanceCoordinator _persistance;
        public TestCoordinator(
            TestSessionManager sessionManager, 
            IAcquisitionStateMachine acquisition,
            SwitchTestEngine switchTestEngine,
            PersistanceCoordinator persistance)
        {
            _sessionManager = sessionManager;
            _acquisition = acquisition;
            _switchTestEngine = switchTestEngine;
            _persistance = persistance;
        }

        public async Task<CoordinatedTestResult> RunTestAsync(Specification specification, TestConfiguration configuration, CancellationToken cancellationToken = default)
        {
            var session = _sessionManager.Start();

            var curve = await _acquisition.ExecuteAsync(cancellationToken);

            var result = _switchTestEngine.Execute(new TestExecutionRequest
            {
                Curve = curve,
                Specification = specification,
                Configuration = configuration,
            });

            _sessionManager.Complete(session);

            await _persistance.SaveAsync(session, result, curve);

            return new CoordinatedTestResult
            {
                Session = session,
                Result = result
            };
        }
    }
}
