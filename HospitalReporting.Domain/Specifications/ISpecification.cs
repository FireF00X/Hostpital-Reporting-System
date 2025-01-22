using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitalReporting.Domain.Specifications
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>> Critria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }
        public Expression<Func<T, object>> OrderByAsc { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagenated { get; set; }
    }
}
