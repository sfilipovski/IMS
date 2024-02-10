using IMS.Domain.Common;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private DbSet<T> entities;

    public RepositoryBase(ApplicationDbContext context, DbSet<T> entities)
    {
        _context = context;
        this.entities = _context.Set<T>();
    }

    public T Create(T entity)
    {
        if(entity == null)
        {
            throw new ArgumentNullException("Can't create null entity");
        }

        entities.AddAsync(entity);
        _context.SaveChanges();

        return entity;
    }

    public T Delete(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Can't delete null entity");
        }

        entities.Remove(entity);
        _context.SaveChanges();

        return entity;
    }

    public T Get(Guid id)
    {
        return entities.SingleOrDefault(x => x.Id == id);
    }

    public ICollection<T> GetAll()
    {
        return entities.ToList();
    }

    public T Update(T entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException("Can't update null entity");
        }

        entities.Update(entity);
        _context.SaveChanges();

        return entity;
    }
}
