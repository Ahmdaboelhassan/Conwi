using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Extinctions
{
    public static class DataBaseServiceExtinction 
    {
        public static IServiceCollection AddDataBase(this IServiceCollection services , 
             IConfiguration Config){
          

         string? ConnectionStrings = Config.GetConnectionString("DefaultConnection");
         if(string.IsNullOrEmpty(ConnectionStrings))  throw new NullReferenceException("You should Provide ConnecionStrings");
        
          //Add Sql Server
          services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(ConnectionStrings));

         return services;
        }
    }
}