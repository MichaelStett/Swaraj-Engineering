using Microsoft.Extensions.DependencyInjection;

using Swaraj.API.Endpoints.Authorization;

using Swashbuckle.AspNetCore.Filters;

namespace Swaraj.API
{
    public static class _DependencyInjection
    {
        public static IServiceCollection AddDependency(IServiceCollection services)
        {

            return services;
        }
    }
}
