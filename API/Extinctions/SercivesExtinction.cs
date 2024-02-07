using API.Helper;
using API.Services;
using API.Services.IServices;

namespace API.Extinctions
{
    public static class SercivesExtinction
    {
        public static IServiceCollection AddApplicaionServices (this IServiceCollection services , 
            IConfiguration config){
          services.AddControllers();
          services.AddEndpointsApiExplorer();       

          services.AddScoped<IAuthService, AuthService>();
          
          services.AddScoped<IEmailService, EmailService>();
          
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

          services.Configure<EmailSettings>(config.GetSection("Email"));

          return services;
        }
    }
}