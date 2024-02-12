using IMS.Domain.Identity;

namespace IMS.Repository.Interface;

public interface IAccountRepository
{
    Customer GetCustomerById(string id);
}
