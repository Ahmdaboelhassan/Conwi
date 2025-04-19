using System.Linq.Expressions;

namespace Domain.IRepository;

public interface IReposetory<T>
{
    // Queries
    Task<T> Get(Expression<Func<T,bool>> criteria, params string[] includes);
    Task<List<T>> GetAll(params string[]? includes);
    Task<List<T>> GetAll(Expression<Func<T, bool>> criteria, params string[]? includes);
    Task<IEnumerable<O>> SelectAll<O>(Expression<Func<T, bool>> criteria, Expression<Func<T, O>> columns, params string[]? includes);
    Task<bool> Exists(Expression<Func<T, bool>> criteria);
    Task<T> DapperGetAsync(string table, string? where = null);
    Task<IEnumerable<T>> DapperGetAllAsync(string table , string? where = "");

    // Command
    Task AddAsync(T element);
    void Delete(T element);
    void Update(T element);

}
