using HospitalReporting.Domain.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.InterfacesRepository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyList<TEntity>> GetAllWithSpecsAsync(ISpecification<TEntity> spec);
        Task<TEntity> GetByIdWithSpecsAsync(ISpecification<TEntity> spec);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> Delete(string id);
    }
}
