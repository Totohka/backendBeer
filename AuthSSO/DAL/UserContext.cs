using AuthSSO.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Model.Entities;

namespace DAL
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<WhiteIp> WhiteIps { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                        warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<RefreshToken>()
                .HasOne(r => r.User)
                .WithOne(u => u.RefreshToken)
                .HasForeignKey<User>(u => u.RefreshTokenUid);

            modelBuilder.Entity<Application>().HasData(
                new Application 
                { 
                    Name = "Volga-Tracker",
                    Description = "Приложения для списка задач, которые нужно сделать для восстановления Волги",
                    DateLastCheck = null,
                    DateBreak = null,
                    Version = "v1",
                    Ip = "195.133.28.197",
                    IsWork = true,
                    TypeApplication = EnumTypeApplication.Backend,
                    Port = 10,
                    IsActive = true,
                },
                new Application
                {
                    Name = "volga_tracker",
                    Description = "База данных для волга-трекер",
                    DateLastCheck = null,
                    DateBreak = null,
                    Version = "v1",
                    Ip = "195.133.28.197",
                    IsWork = true,
                    Login = "postgres",
                    Password = "3G4rc3093jBlYgEaha/fOw==",
                    TypeApplication = EnumTypeApplication.Database,
                    Port = 5432,
                    IsActive = true,
                },
                new Application
                {
                    Name = "UserBeer",
                    Description = "База данных для пользователей",
                    DateLastCheck = null,
                    DateBreak = null,
                    Version = "v1",
                    Ip = "195.133.28.197",
                    IsWork = true,
                    Login = "postgres",
                    Password = "3G4rc3093jBlYgEaha/fOw==",
                    TypeApplication = EnumTypeApplication.Database,
                    Port = 5432,
                    IsActive = true,
                });
            modelBuilder.Entity<User>().HasData(
                new User 
                { 
                    FirstName = "Дмитрий",
                    LastName = "Патюков",
                    MiddleName = "Анатольевич",
                    Email = null,
                    ApiKeyUid = null,
                    Login = "Totohka",
                    IsActive = true,
                    RefreshTokenUid = null
                },
                new User 
                {
                    FirstName = "Эдуард",
                    LastName = "Новиков",
                    MiddleName = "Дмитриевич",
                    Email = null,
                    ApiKeyUid = null,
                    Login = "Nedoff",
                    IsActive = true,
                    RefreshTokenUid = null
                },
                new User
                {
                    FirstName = "Степан",
                    LastName = "Кондрашов",
                    MiddleName = "Андреевич",
                    Email = null,
                    ApiKeyUid = null,
                    Login = "Stepan",
                    IsActive = true,
                    RefreshTokenUid = null
                },
                new User
                {
                    FirstName = "Кирилл",
                    LastName = "Шилов",
                    MiddleName = "Александрович",
                    Email = null,
                    ApiKeyUid = null,
                    Login = "Kirill",
                    IsActive = true,
                    RefreshTokenUid = null
                });
        }
    }
}
