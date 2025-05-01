
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shark_Tech.DAL;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
      var entity= await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

    }

    public  async Task<IReadOnlyList<T>> GetAllAsync() =>await _context.Set<T>().AsNoTracking().ToListAsync();


    public async Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
       var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        return entity;
    }

    public async Task<T> GetByIdAsync(Guid id, params Expression<Func<T, object>>[] includes)
    {
        var query = _context.Set<T>().AsQueryable();
        foreach (var include in includes)
        {
            query = query.Include(include);
        }
        var entity = await query.FirstOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
