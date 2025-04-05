using System.Data;

namespace Application.IRepository;

public interface IUnitOfWork
{
    IUserRepo Users { get; }
    IPostRepo Posts { get; }
    IUserFollowRepo UserFollow { get; }
    IUserLikeRepo UserLike { get; }
    IMessagesRepo Messages { get; }

    int SaveChanges();
    Task SaveChangesAsync();
   
    IDbTransaction StartTransaction();
}
