﻿using Domain.Entity;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;
internal class UserRepo : Repository<AppUser>, IUserRepo
{
    public UserRepo(ApplicationDbContext db, IConfiguration config) : base(db, config) { }
}
