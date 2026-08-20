using Checker.Domain.Enums;

namespace Checker.API.Contracts.Requests
{
    public class CreateMeasurementLimitRequest
    {
        public MeasurementType MeasurementType { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
    }
}
