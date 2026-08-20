using Checker.Domain.Enums;

namespace Checker.API.Contracts.Responses
{
    public class SpecificationResponse
    {
        public Guid Id { get; set; }
        public string PartNumber { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;
        public CircuitType CircuitType { get; set; }
        public List<MeasurementLimitResponse> Limits { get; set; } = [];
    }
}
