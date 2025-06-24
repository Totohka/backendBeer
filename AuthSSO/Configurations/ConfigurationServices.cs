using BL.Services.Admins;
using BL.Services.Admins.Base;
using BL.Services.Emails;
using BL.Services.Tokens;
using BL.Services.Users;
using Model.Entities;

namespace AuthSSO.Configurations
{
    public static class ConfigurationServices
    {
        public static void Configuration(IServiceCollection services)
        {
            //BL
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccessTokenService, AccessTokenService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            //Admin BL
            services.AddScoped<IBaseAdminService<ApiKey>, BaseAdminService<ApiKey>>();
            services.AddScoped<IBaseAdminService<Application>, BaseAdminService<Application>>();
            services.AddScoped<IBaseAdminService<RefreshToken>, BaseAdminService<RefreshToken>>();
            services.AddScoped<IBaseAdminService<Role>, BaseAdminService<Role>>();
            services.AddScoped<IUserAdminService, UserAdminService>();
            services.AddScoped<IBaseAdminService<UserRole>, BaseAdminService<UserRole>>();
            services.AddScoped<IBaseAdminService<WhiteIp>, BaseAdminService<WhiteIp>>();
        }
    }
}
