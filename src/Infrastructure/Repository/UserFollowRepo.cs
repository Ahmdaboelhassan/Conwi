using Application.IRepository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;
public class UserFollowRepo : Repository<UserFollow>, IUserFollowRepo
{
    public UserFollowRepo(ApplicationDbContext db, IConfiguration config) : base(db, config)
    {
    }
}
