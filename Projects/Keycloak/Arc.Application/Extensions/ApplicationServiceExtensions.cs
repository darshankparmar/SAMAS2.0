using Microsoft.Extensions.DependencyInjection;
using Arc.Application.Interfaces;
using Arc.Application.Services;

namespace Arc.Application.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IKeycloakService, KeycloakService>();
            return services;
        }
    }
}
