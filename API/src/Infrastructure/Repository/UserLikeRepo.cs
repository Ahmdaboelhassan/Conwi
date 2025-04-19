using Domain.Entity;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class UserLikeRepo : Repository<UserLike>, IUserLikeRepo
{
    public UserLikeRepo(ApplicationDbContext db, IConfiguration config) : base(db, config) { }
    
}
