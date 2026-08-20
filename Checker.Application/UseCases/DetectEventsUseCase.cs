using Checker.Domain.Entities;
using Checker.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.UseCases
{
    public class DetectEventsUseCase
    {
        public ContactEvents Execute(ForceTravelCurve curve)
        {
            var events = new ContactEvents();

            for (var i = 1; i < curve.Points.Count; i++)
            {
                var previous = curve.Points[i - 1];
                var current = curve.Points[i];
                bool previousNc = previous.ContactState.HasNc();
                bool currentNc = current.ContactState.HasNc();
                bool previousNo = previous.ContactState.HasNo();
                bool currentNo = current.ContactState.HasNo();

                if (events.FTLoseNC1 is null && previousNc && !currentNc)
                {
                    events.FTLoseNC1 = i;
                    continue;
                }

                if(events.FTLoseNC1 is not null &&
                    events.FTMakeNO1 is null &&
                    !previousNo && currentNo)
                {
                    events.FTMakeNO1 = i;
                    continue;
                }

                if(events.FTMakeNO1 is null && events.FTLoseNO1 is null &&
                    previousNo && !currentNo)
                {
                    events.FTMakeNO1 = i;
                    continue;
                }

                if(events.FTLoseNO1 is not null && events.FTMakeNC1 is null &&
                    !previousNc && currentNc)
                {
                    events.FTMakeNC1 = i;
                    continue;
                }

                if(events.FTMakeNC1 is not null && events.FTLoseNC2 is null &&
                    previousNc && !currentNc)
                {
                    events.FTLoseNC2 = i;
                    continue;
                }

                if(events.FTLoseNC2 is not null &&
                    events.FTMakeNO2 is null &&
                    !previousNo && currentNo)
                {
                    events.FTMakeNO2 = i;
                    continue;
                }
            }

            return events;
        }
    }
}
