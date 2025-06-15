using BL.Services.Admins.Base;
using DAL.Repositories;
using Model.Entities;

namespace BL.Services.Admins
{
    public interface IUserAdminService : IBaseAdminService<User>
    {
        public Task ChangeActiveUser(Guid uid);
    }
    public class UserAdminService : BaseAdminService<User>, IUserAdminService
    {
        public UserAdminService(IBaseRepository<User> baseRepository) : base(baseRepository)
        {
        }

        public async Task ChangeActiveUser(Guid uid)
        {
            var user = await _baseRepository.GetAsync(uid);
            if (user != null)
            {
                await _baseRepository.UpdateAsync(user);
            }
            else
            {
                
            }
        }
    }
}
