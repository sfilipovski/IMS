using IMS.Domain.Common;

namespace IMS.Repository.Interface;

public interface IRepository<T> where T : BaseEntity
{
    ICollection<T> GetAll();
    T Get(int? id);
    void Create(T entity);
    void Update(T entity);
    void Delete(T entity);

}
