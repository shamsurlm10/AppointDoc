using AppointDoc.Application.Interfaces.Base;
using AppointDoc.Application.Repositories.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointDoc.Application.Services.Base
{
    public class Manager<T> : IManager<T> where T : class
    {
        private readonly IRepository<T> _repo;

        public Manager(IRepository<T> repo)
        {
            _repo = repo;
        }

        public async Task<bool> AddAsync(T entity)
        {
            return await _repo.AddAsync(entity);
        }

        public Task<bool> UpdateAsync(T entity)
        {
            return _repo.UpdateAsync(entity);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repo.DeleteAsync(id);
        }

        public async Task<T> FindByAsync(Guid id)
        {
            return await (_repo.FindByAsync(id));
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
