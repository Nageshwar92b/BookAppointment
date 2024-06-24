using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Appointment.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<T> FirstAsync(Expression<Func<T, bool>> predicate, bool getDefault = true);
        Task<int> AddAsync(T entity);
    }

}
