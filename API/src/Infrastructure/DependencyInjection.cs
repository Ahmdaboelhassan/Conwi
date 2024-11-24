using Application.Helper;
using Application.IRepository;
using Application.IServices;
using Domain.Entity;
using Infrastructure.Data;
using Infrastructure.Repository;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration Config)
        {


            string? ConnectionStrings = Config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(ConnectionStrings)) throw new NullReferenceException("You should Provide ConnecionStrings");

            //Add Sql Server
           // services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(ConnectionStrings));

           // postgress
            services.AddDbContext<ApplicationDbContext>(opt => opt.UseNpgsql(ConnectionStrings));

            


            services.AddIdentity<AppUser, IdentityRole>().AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(JwtOptions =>
            {
                JwtOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = Config["JWT:Issuer"],
                    ValidAudience = Config["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.Configure<JWT>(Config.GetSection("JWT"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
          
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IPhotoService, PhotoService>();

            services.AddScoped<ITokenService, TokenService>();


         return services;
        }

    }
}
