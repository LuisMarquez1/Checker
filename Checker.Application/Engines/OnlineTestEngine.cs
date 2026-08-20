using Checker.Application.UseCases;
using Checker.Domain.Entities;

namespace Checker.Application.Engines
{
    public class OnlineTestEngine
    {
        private readonly AcquireCurveUseCase _acquireCurve;
        private readonly SwitchTestEngine _testEngine;

        public OnlineTestEngine(AcquireCurveUseCase acquireCurve, SwitchTestEngine testEngine)
        {
            _acquireCurve = acquireCurve;
            _testEngine = testEngine;
        }

        public async Task<TestExecutionResult> ExecuteAsync(
            Specification specification,
            int sampleCount,
            TestConfiguration configuration,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(specification);

            ArgumentNullException.ThrowIfNull(configuration);

            if (sampleCount <= 0)
                throw new ArgumentOutOfRangeException(nameof(sampleCount), "Sample count must be greater than zero.");

            var curve = await _acquireCurve.ExecuteAsync(sampleCount, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();

            return _testEngine.Execute(new TestExecutionRequest
                {
                    Curve =
                        curve,

                    Specification =
                        specification,

                    Configuration =
                        configuration
                });
        }
    }
}