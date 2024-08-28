using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Application.Helper;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicaionLayer(this IServiceCollection services,
            IConfiguration cfg)
        {
            services.Configure<EmailSettings>(cfg.GetSection("Email"));

            services.Configure<CloudinaryAccount>(cfg.GetSection("CloudinaryAccount"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
