using Domain.Entity;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository
{
    class NotificationRepo : Repository<Notification>, INotificationRepo
    {
        public NotificationRepo(ApplicationDbContext db, IConfiguration config) : base(db, config)
        {
        }
    }
}
