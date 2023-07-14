using HotelListing.Data;
using HotelListing.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HotelListing.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<T> _db;

    public GenericRepository(DatabaseContext context)
    {
        _context = context;
        _db = _context.Set<T>();
    }

    public async Task Delete(int id)
    {
        var entity = await _db.FindAsync(id);
        _db.Remove(entity);
    }

    public void DeleteRane(IEnumerable<T> entities)
    {
        _db.RemoveRange(entities);
    }

    public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
    {
        IQueryable<T> query = _db;

        if (expression != null)
        {
            query = query.Where(expression);
        }

        if (includes != null)
        {
            foreach (var includePropery in includes)
            {
                query.Include(includePropery);
            }
        }

        if(orderBy != null)
        {
            query = orderBy(query);
        }

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> expression, List<string> includes)
    {
        IQueryable<T> query = _db;

        if(includes != null)
        {
            foreach (var includePropery in includes)
            {
                query.Include(includePropery);
            }
        }

        return await query.AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public async Task Insert(T entity)
    {
        await _db.AddAsync(entity);
    }

    public async Task InsertRange(IEnumerable<T> entities)
    {
        await _db.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _db.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }
}
