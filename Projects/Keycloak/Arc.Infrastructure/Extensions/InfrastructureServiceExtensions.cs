using Microsoft.Extensions.DependencyInjection;
using Arc.Infrastructure.Repositories;
using Arc.Domain.Interfaces;

namespace Arc.Infrastructure.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpClient<IKeycloakRepository, KeycloakRepository>();
            return services;
        }
    }
}
