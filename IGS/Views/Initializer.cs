using IGS.DAL.Interfaces;
using IGS.DAL.Repositories;
using IGS.Domain.Entity;
using IGS.Service.Implementations;

namespace IGS
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBaseRepository<User>, UserRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<AccountService>();
        }
    }
}
