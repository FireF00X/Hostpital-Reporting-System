using HospitalReporting.Domain.InterfacesRepository;
using HospitalReporting.Infrastructure.Context;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private Hashtable _repos;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            _repos = new Hashtable();
        }
        public async Task<int> CompleteAsync()
        => await _context.SaveChangesAsync();

        public IGenericRepository<TEntity> CreateRepo<TEntity>() where TEntity : class
        {
            var key = typeof(TEntity).Name;
            if (!_repos.ContainsKey(key))
            {
                var myObj = new GenericRepository<TEntity>(_context);
                _repos.Add(key, myObj);
            }
            return _repos[key] as IGenericRepository<TEntity>;
        }

        public ValueTask DisposeAsync()
        => _context.DisposeAsync();
    }
}
