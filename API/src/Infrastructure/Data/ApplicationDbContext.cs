using Infrastructure.Helper;
using Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Post> Posts { get; set; }
        public DbSet<UserFollow> UserFollow { get; set; }
        public DbSet<UserLike> UserLike { get; set; }
        public DbSet<Message> Messages { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .Property(u => u.DateOfBirth)
                .HasConversion(new DateConverter());
        }
    }
}