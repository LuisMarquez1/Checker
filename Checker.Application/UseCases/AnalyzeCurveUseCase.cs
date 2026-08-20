using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Checker.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.UseCases
{
    public class AnalyzeCurveUseCase
    {
        private readonly IForceTravelAnalyzer _analyzer;

        public AnalyzeCurveUseCase(IForceTravelAnalyzer analizer)
        {
            _analyzer = analizer;
        }

        public CurveAnalysisResult Execute(ForceTravelCurve curve, ContactEvents events)
        {
            if(events.FTLoseNO1 is null)
                throw new InvalidOperationException("Release event FTLoseNO1 not found.");

            if(events.FTMakeNC1 is null)
                throw new InvalidOperationException("MakeNC1 event FTMakeNC1 not found.");
            if (events.FTLoseNC2 is null)
                throw new InvalidOperationException("Operate event FTLoseNC2 Not found.");

            if (events.FTMakeNO2 is null)
                throw new InvalidOperationException("Operate events FTMakeNO2 not found.");

            if(events.FTLoseNC1 is null)
                throw new InvalidOperationException("LoseNC1 event not found.");

            if(events.FTMakeNO1 is null)
                throw new InvalidOperationException("MakeNO1 event not found.");

            var releasePoint = _analyzer.FindReleasePoint(curve, events.FTLoseNO1.Value, events.FTMakeNC1.Value);
            var operatePoint = _analyzer.FindOperatePoint(curve, events.FTLoseNC1.Value, events.FTMakeNO1.Value);

            return new CurveAnalysisResult
            {
                OperatePointIndex = operatePoint,
                ReleasePointIndex = releasePoint
            };
        }
    }
}
