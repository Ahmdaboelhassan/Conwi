﻿using Application.IRepository;
using Dapper;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.Repository;

public class Repository<T> : IReposetory<T> where T : class
{
    private readonly DbSet<T> _set;
    private readonly string _connectionStrings;

    public Repository(ApplicationDbContext db, IConfiguration config) 
    {
        _set = db.Set<T>();
        _connectionStrings = config.GetConnectionString("DefaultConnection");
    }
    public Task<T> GetAsync(Expression<Func<T, bool>> criteria, params string[] includes)
    {
       var query = _set.Where(criteria);
        foreach (var item in includes) { 
            
            query = query.Include(item);
        }

        return query.FirstOrDefaultAsync();
    }
    public Task<List<T>> GetAll(Expression<Func<T, bool>> criteria, params string[]? includes)
    {
        var query = _set
            .Where(criteria)
            .AsNoTracking();

        foreach (var item in includes)
        {
            query = query.Include(item);
        }

        return query.ToListAsync();
    }
    public Task<T> DapperGetAsync(string table, string? where = null)
    {
        var conn = CreateDapperConnection();
        return conn.QueryFirstOrDefaultAsync<T>($"SELECT * FROM {table} WHERE {where}"); ;
    }
    public Task<IEnumerable<T>> DapperGetAllAsync(string table, string? where = "")
    {
        var conn = CreateDapperConnection();
        return conn.QueryAsync<T>($"SELECT * FROM {table} WHERE {where}"); ;
    }

    public bool Exists(Expression<Func<T, bool>> criteria)
    {
        return _set.Where(criteria).Count() > 0;
    }
    public Task AddAsync(T element)
    {
       _set.AddAsync(element);
       return Task.CompletedTask;
    }
    public void Update(T element)
    {
        _set.Update(element);
    }
    public void Delete(T element)
    {
        _set.Remove(element);
    }

    private IDbConnection CreateDapperConnection() => new SqlConnection(_connectionStrings);
}
