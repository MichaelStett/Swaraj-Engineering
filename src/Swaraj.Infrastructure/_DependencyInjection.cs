using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swaraj.Domain;
using Swaraj.Domain.Data;

namespace Swaraj.Infrastructure
{
    public static class _DependencyInjection
    {
        const string ContextConnectionString = "Context";

        public static IServiceCollection AddDependency(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();

            services.AddDbContext<Context>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString(ContextConnectionString));
                opt.EnableSensitiveDataLogging();
            });

            services.AddScoped<IUserContext, Context>();
            services.AddScoped<IWorkItemContext, Context>();

            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
