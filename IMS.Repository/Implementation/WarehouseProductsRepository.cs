using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository.Implementation;

public class WarehouseProductsRepository : IRepository<WarehouseProducts>
{

    private readonly ApplicationDbContext applicationDbContext;

    public WarehouseProductsRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public void Create(WarehouseProducts entity)
    {
        applicationDbContext.Add(entity);
        applicationDbContext.SaveChanges();
    }

    public void Delete(WarehouseProducts entity)
    {
        applicationDbContext.Remove(entity);
        applicationDbContext.SaveChanges();
    }

    public WarehouseProducts Get(int? id)
    {
        return applicationDbContext.WarehouseProducts.SingleOrDefault(x => x.Id == id);
    }

    public ICollection<WarehouseProducts> GetAll()
    {
        return applicationDbContext.WarehouseProducts
            .AsNoTracking()
            .Include(x => x.Warehouse)
            .Include(x => x.WarehouseProduct)
            .ToList();
    }

    public void Update(WarehouseProducts entity)
    {
        applicationDbContext.Update(entity);
    }
}
