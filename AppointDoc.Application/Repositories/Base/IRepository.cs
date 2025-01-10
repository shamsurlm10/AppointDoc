using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Repositories.Base
{
    public interface IRepository<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindByAsync(Guid id);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(Guid id);
    }
}
