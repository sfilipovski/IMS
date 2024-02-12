using IMS.Domain.Identity;
using IMS.Repository.Interface;

namespace IMS.Repository.Implementation;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Customer GetCustomerById(string id)
    {
        return _context.Customers.SingleOrDefault(x => x.Id == id);
    }
}
