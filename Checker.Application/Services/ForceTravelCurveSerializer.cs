using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Checker.Domain.Entities;

namespace Checker.Application.Services
{
    public class ForceTravelCurveSerializer
    {
        public string Serialize(ForceTravelCurve curve) { 
            return JsonSerializer.Serialize(curve);
        }

        public ForceTravelCurve Deserialize(string json)
        {
            return JsonSerializer.Deserialize<ForceTravelCurve>(json)! ?? new ForceTravelCurve();
        }
    }
}
