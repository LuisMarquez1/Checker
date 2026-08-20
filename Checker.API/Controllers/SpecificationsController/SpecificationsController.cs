using Microsoft.AspNetCore.Mvc;
using Checker.Application.Interfaces;
using Checker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Checker.API.Contracts.Requests;
using Checker.API.Contracts.Responses;

namespace Checker.API.Controllers.SpecificationsController
{
    [ApiController]
    [Route("api/specifications")]
    public class SpecificationsController : Controller
    {
        private readonly ISpecificationRepository _repository;

        public SpecificationsController(ISpecificationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{partNumber}")]
        public async Task<IActionResult> Get(string partNumber)
        {
            var specification = await _repository.GetByPartNumberAsync(partNumber);

            if (specification is null)
                return NotFound();

            var response = new SpecificationResponse
            {
                Id = specification.Id,
                PartNumber = specification.PartNumber,
                Revision = specification.Revision,
                CircuitType = specification.CircuitType,
                Limits = specification.Limits.Select(x => new MeasurementLimitResponse
                {
                    MeasurementType = x.MeasurementType,
                    Minimum = x.Minimum,
                    Maximum = x.Maximum,
                }).ToList()
            };

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateSpecificationRequest request)
        {
            var specification = new Specification
            {
                Id = Guid.NewGuid(),
                PartNumber = request.PartNumber,
                Revision = request.Revision,
                CircuitType = request.CircuitType,
                Limits = request.Limits.Select(x => new MeasurementLimit
                {
                    Id = Guid.NewGuid(),
                    MeasurementType = x.MeasurementType,
                    Minimum = x.Minimum,
                    Maximum = x.Maximum,
                }).ToList()
            };
            await _repository.AddAsync(specification);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var specifications = await _repository.GetAllAsync();

            return Ok(specifications);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateSpecificationRequest request)
        {
            var specification = await _repository.GetByIdAsync(id);

            if (specification is null)
                return NotFound();

            specification.PartNumber = request.PartNumber;
            specification.Revision = request.Revision;
            specification.CircuitType = request.CircuitType;

            await _repository.updateAsync(specification);

            return Ok();
        }
    }
}
