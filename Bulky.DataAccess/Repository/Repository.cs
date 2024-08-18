using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal readonly DbSet<T> DbSet;

    public Repository(ApplicationDbContext db)
    {
        _db = db; 
        this.DbSet = _db.Set<T>();
        _db.Products.Include(u => u.Category).Include(u => u.CategoryId);
    }
    
    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query;
        if (tracked) {
            query= DbSet;
                
        }
        else {
            query = DbSet.AsNoTracking();
        }

        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties)) {
            foreach (var includeProp in includeProperties
                         .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)) {
                query = query.Include(includeProp);
            }
        }
        return query.FirstOrDefault()!;
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
    {
        IQueryable<T> query = DbSet;
        if (filter != null) {
            query = query.Where(filter);
        }
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach(var includeProp in includeProperties
                        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return query.ToList();
    }

    public void Add(T entity) { DbSet.Add(entity); } 
    public void Remove(T entity) { DbSet.Remove(entity); } 
    public void RemoveRange(IEnumerable<T> entity) { DbSet.RemoveRange(entity); }
}