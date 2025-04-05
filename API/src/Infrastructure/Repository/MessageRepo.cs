using Application.IRepository;
using Domain.Entity;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class MessageRepo : Repository<Message>, IMessagesRepo
{
    public MessageRepo(ApplicationDbContext db, IConfiguration config) : base(db, config)
    {
    }
}
