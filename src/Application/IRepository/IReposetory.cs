
using System.Linq.Expressions;

namespace Application.IRepository;

public interface IReposetory<T>
{
    // Queries
    Task<T> GetAsync(Expression<Func<T,bool>> criteria, params string[] includes);
    Task<List<T>> GetAll(Expression<Func<T, bool>> criteria, params string[]? includes);
    Task<T> DapperGetAsync(string table, string? where = null);
    Task<IEnumerable<T>> DapperGetAllAsync(string table , string? where = "");
    bool Exists(Expression<Func<T, bool>> criteria);

    // Command
    Task AddAsync(T element);
    void Delete(T element);
    void Update(T element);

}
