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

          services.AddScoped<IUserService, UserService>();

          services.AddScoped<IPhotoService, PhotoService>();
          
          services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

          services.Configure<EmailSettings>(config.GetSection("Email"));

          services.Configure<CloudinaryAccount>(config.GetSection("CloudinaryAccount"));

          services.AddCors(
                 options => options.AddPolicy("AllowedAudience" , 
                    policy => {
                        policy.AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials()
                              .WithOrigins("http://localhost:4200");
                    }
                 )
            );

          return services;
        }
    }
}