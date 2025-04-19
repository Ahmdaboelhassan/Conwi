using System.Data;

namespace Domain.IRepository;

public interface IUnitOfWork
{
    IUserRepo Users { get; }
    IPostRepo Posts { get; }
    IUserFollowRepo UserFollow { get; }
    IUserLikeRepo UserLike { get; }
    IMessagesRepo Messages { get; }
    INotificationRepo Notification { get;}

    int SaveChanges();
    Task SaveChangesAsync();
   
    IDbTransaction StartTransaction();
}
