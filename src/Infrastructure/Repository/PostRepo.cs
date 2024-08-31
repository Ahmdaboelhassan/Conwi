using Application.IRepository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

internal class PostRepo :Repository<Post> ,IPostRepo
{
    public PostRepo(ApplicationDbContext db , IConfiguration config) : base(db , config){}
}
