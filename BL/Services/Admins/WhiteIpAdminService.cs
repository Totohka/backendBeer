using BL.Services.Admins.Base;
using DAL.Entities;
using DAL.Repositories;
using Model.Entities;

namespace BL.Services.Admins
{
    public interface IWhiteIpAdminService : IBaseAdminService<WhiteIp>
    {

    }

    public class WhiteIpAdminService : BaseAdminService<WhiteIp>, IWhiteIpAdminService
    {
        public WhiteIpAdminService(IBaseRepository<WhiteIp> whiteIpRepository) : base(whiteIpRepository)
        {

        }
    }
}
