using Checker.Domain.Enums;

namespace Checker.API.Contracts.Requests
{
    public class CreateSpecificationRequest
    {
        public string PartNumber { get; set; } = string.Empty;
        public string Revision { get; set; } = string.Empty;
        public CircuitType CircuitType { get; set; }
        public List<CreateMeasurementLimitRequest> Limits { get; set; } = [];
    }
}
