using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;

namespace Checker.Application.Services
{
    public class PersistanceCoordinator
    {
        private readonly ITestSessionRepository _sessionRepository;
        private readonly ITestResultRepository _resultRepository;
        private readonly IForceTravelCurveRepository _curveRepository;
        private readonly ForceTravelCurveSerializer _serializer;

        public PersistanceCoordinator(
            ITestSessionRepository sessionRepository,
            ITestResultRepository resultRepository,
            IForceTravelCurveRepository curveRepository,
            ForceTravelCurveSerializer serializer)
        {
            _sessionRepository = sessionRepository;
            _resultRepository = resultRepository;
            _curveRepository = curveRepository;
            _serializer = serializer;
        }

        public async Task SaveAsync(TestSession session, TestExecutionResult result, ForceTravelCurve curve)
        {
            await _sessionRepository.AddAsync(session);
            await _resultRepository.AddAsync(result);

            var storedCurve = new StoredForceTravelCurve
            {
                Id = Guid.NewGuid(),
                SessionId = session.Id,
                CurveJson = _serializer.Serialize(curve)
            };

            await _curveRepository.AddAsync(storedCurve);
        }
    }
}
