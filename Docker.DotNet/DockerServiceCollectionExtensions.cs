#if !NET45
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Docker.DotNet
{
    public static class DockerServiceCollectionExtensions
    {
        public static IServiceCollection AddDocker(this IServiceCollection services,
            DockerClientConfiguration clientConfiguration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            DockerClient client = clientConfiguration.CreateClient();

            services.Add(new ServiceDescriptor(typeof(IDockerClient), client));

            return services;
        }
    }
}
#endif