using Checker.Domain.Enums;

namespace Checker.API.Contracts.Responses
{
    public class MeasurementLimitResponse
    {
        public MeasurementType MeasurementType { get; set; }
        public double? Minimum { get; set; }
        public double? Maximum { get; set; }
    }
}
