using DAL.Repositories;
using Model.Entities;

namespace AuthSSO.Configurations
{
    public class ConfigurationRepositories
    {
        public static void Configuration(IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<Application>, BaseRepository<Application>>();
            services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            services.AddScoped<IBaseRepository<RefreshToken>, BaseRepository<RefreshToken>>();
            services.AddScoped<IBaseRepository<ApiKey>, BaseRepository<ApiKey>>();
            services.AddScoped<IBaseRepository<Role>, BaseRepository<Role>>();
            services.AddScoped<IBaseRepository<UserRole>, BaseRepository<UserRole>>();
            services.AddScoped<IBaseRepository<WhiteIp>, BaseRepository<WhiteIp>>();
        }
    }
}
