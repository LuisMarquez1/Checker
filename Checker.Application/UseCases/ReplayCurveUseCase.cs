using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.UseCases
{
    public class ReplayCurveUseCase
    {
        private readonly ICurveImporter _importer;

        public ReplayCurveUseCase(ICurveImporter importer)
        {
            _importer = importer;
        }

        public ForceTravelCurve Execute(string filePath)
        {
            return _importer.Import(filePath);
        }
    }
}
