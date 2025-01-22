using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Domain.Specifications;
using HospitalReporting.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository
{

    public class GenericRepository<T>(AppDbContext _dbContext) : IGenericRepository<T> where T : class
    {
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task<int> Delete(string id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null) return 0;
            _dbContext.Remove(entity);
            return 1;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecsAsync(ISpecification<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpecsAsync(ISpecification<T> spec)
        {
            return await SpecificationsEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            if (entity is null) return 0;
            _dbContext.Update(entity);
            return 1;
        }
    }
}

