using Application.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db , IConfiguration config)
        {
            Users = new UserRepo(db , config);
            Posts = new PostRepo(db, config);
            UserFollow = new UserFollowRepo(db , config);
            UserLike = new UserLikeRepo(db , config);
            _db = db;
        }

        public IUserRepo Users { get; private set; }

        public IPostRepo Posts { get; private set; }

        public IUserFollowRepo UserFollow{ get; private set; }
        public IUserLikeRepo UserLike{ get; private set; }

        public IDbTransaction StartTransaction()
        {
            var transaction = _db.Database.BeginTransaction();
            return transaction.GetDbTransaction();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

    }
}
