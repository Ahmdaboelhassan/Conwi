using System.Data;

namespace Application.IRepository;

public interface IUnitOfWork
{
    IUserRepo Users { get; }
    IPostRepo Posts { get; }
    IUserFollowRepo UserFollow { get; }

    int SaveChanges();
    IDbTransaction StartTransaction();
}
