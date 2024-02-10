using IMS.Domain.Common;

namespace IMS.Repository.Interface;

public interface IRepository<T> where T : BaseEntity
{
    ICollection<T> GetAll();
    T Get(int id);
    Task<T> Create(T entity);
    T Update(T entity);
    T Delete(T entity);

}
