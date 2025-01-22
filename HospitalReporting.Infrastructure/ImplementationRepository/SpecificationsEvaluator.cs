using HospitalReporting.Domain.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Infrastructure.ImplementationRepository
{
    public static class SpecificationsEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> sequence, ISpecification<TEntity> spec)
        {
            var query = sequence;

            if (spec.Critria is not null)
                query = query.Where(spec.Critria);

            if (spec.OrderByAsc is not null)
                query = query.OrderBy(spec.OrderByAsc);

            else if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);

            if (spec.IsPagenated)
                query = query.Skip(spec.Skip).Take(spec.Take);


            query = spec.Includes.Aggregate(query, (current, includesQuery) => current.Include(includesQuery));

            return query;
        }
    }
}
