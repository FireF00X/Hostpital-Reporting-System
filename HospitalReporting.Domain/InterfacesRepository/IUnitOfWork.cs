using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.InterfacesRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<TEntity> CreateRepo<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
    }
}
