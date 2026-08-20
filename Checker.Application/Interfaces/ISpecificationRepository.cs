using Checker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checker.Application.Interfaces
{
    public interface ISpecificationRepository
    {
        Task<Specification> GetByIdAsync(Guid id);
        Task updateAsync(Specification specification);
        Task<List<Specification>> GetAllAsync();
        Task<Specification> GetByPartNumberAsync(string partNumber);
        Task AddAsync(Specification specification);
    }
}
