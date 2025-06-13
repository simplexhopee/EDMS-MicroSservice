using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using UserService.Shared.Auth;
using UserService.Shared.MultiTenancy;


namespace UserService.Infrastructure.DbContexts
{
    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();


            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>()
                .UseMySql(config.GetConnectionString("Default"),
        new MySqlServerVersion(new Version(8, 0, 29)));

            var tenant = new TenantContext();
            var user = new CurrentUser();

            return new UserDbContext(optionsBuilder.Options, tenant, user);


        }
    }
}
