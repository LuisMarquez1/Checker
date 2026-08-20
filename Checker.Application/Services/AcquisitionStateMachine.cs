using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Services
{
    public class AcquisitionStateMachine : IAcquisitionStateMachine
    {
        private readonly IDataAcquisitionService _dataSource;
        private readonly IForceTravelRecorder _recorder;
        private readonly ITriggerCondition _trigger;
        private readonly IStopCondition _stopCondition;

        public AcquisitionContext Context { get; } = new();

        public AcquisitionStateMachine(IDataAcquisitionService dataSource, IForceTravelRecorder recorder, ITriggerCondition trigger, IStopCondition stopCondition)
        {
            _dataSource = dataSource;
            _recorder = recorder;
            _trigger = trigger;
            _stopCondition = stopCondition;
        }
        public async Task<ForceTravelCurve> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                Context.State = AcquisitionState.WaitingForTrigger;

                while (!cancellationToken.IsCancellationRequested)
                {
                    var snapshot = _dataSource.Read();

                    if (_trigger.IsTriggered(snapshot))
                        break;

                    await Task.Delay(1, cancellationToken);
                }

                Context.State = AcquisitionState.Acquiring;

                _recorder.Start();

                while (!cancellationToken.IsCancellationRequested)
                {
                    var snapshot = _dataSource.Read();

                    _recorder.Record(snapshot);

                    if (_stopCondition.ShouldStop(_recorder.CurrentCure))
                        break;

                    await Task.Delay(1, cancellationToken);
                }

                Context.State = AcquisitionState.Processing;

                Context.Curve = _recorder.Stop();

                Context.State = AcquisitionState.Completed;

                return Context.Curve;
            }
            catch (Exception ex)
            {
                Context.State = AcquisitionState.Error;

                Context.ErrorMessage = ex.Message;

                throw;
            }
        }
    }
}
