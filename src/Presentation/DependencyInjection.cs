using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationLayer(this IServiceCollection services ,IConfiguration cfg)
        {
            return services;
        }
    }
}
