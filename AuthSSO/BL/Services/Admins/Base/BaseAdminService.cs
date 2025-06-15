using DAL.Entities;
using DAL.Repositories;
using Model.Entities;

namespace BL.Services.Admins.Base
{
    public interface IBaseAdminService<T> where T : BaseEntity
    {
        public Task<T> Get(Guid uid);
        public Task<ResponsePagination<T>> GetAll(int skip, int take);
        public Task<Guid> Create(T item);
        public Task Update(T item);
        public Task Delete(Guid uid);
    }

    public class BaseAdminService<T> : IBaseAdminService<T> 
        where T: BaseEntity
    {
        protected readonly IBaseRepository<T> _baseRepository;
        public BaseAdminService(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public virtual async Task<Guid> Create(T item)
        {
            return await _baseRepository.CreateAsync(item);
        }

        public virtual async Task Delete(Guid uid)
        {
            await _baseRepository.DeleteAsync(uid);
        }

        public virtual async Task<T> Get(Guid uid)
        {
            return await _baseRepository.GetAsync(uid);
        }

        public virtual async Task<ResponsePagination<T>> GetAll(int skip, int take)
        {
            return await _baseRepository.GetPartAsync(item => true, skip, take);
        }

        public virtual async Task Update(T item)
        {
            await _baseRepository.UpdateAsync(item);
        }
    }
}
