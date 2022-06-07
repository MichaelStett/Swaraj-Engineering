using Microsoft.Extensions.DependencyInjection;

using Swaraj.Application.Services;
using Swaraj.Domain;
using Swaraj.Domain.Services;

namespace Swaraj.Application
{
    public static class _DependencyInjection
    {
        public static IServiceCollection AddDependency(IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddSingleton<TokenGenerator>();
            services.AddSingleton<IStringCypher, StringCypher>();

            return services;
        }
    }
}
