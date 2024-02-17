using IMS.Domain.Relationship;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Repository.Implementation;

public class WarehouseProductsRepository : IWarehouseProductsRepository
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
        return applicationDbContext.WarehouseProducts
            .SingleOrDefault(x => x.Id == id);
    }

    public ICollection<WarehouseProducts> GetAll()
    {
        return applicationDbContext.WarehouseProducts
            .Include(x => x.Warehouse)
            .Include(x => x.WarehouseProduct)
            .AsNoTracking()
            .ToList();
    }

    public List<WarehouseProducts> GetByProductId(int productId)
    {
        return this.applicationDbContext.WarehouseProducts.Where(x => x.WarehouseProductId == productId).ToList();
    }

    public WarehouseProducts GetByProductIdAndWarehouseId(int productId, int? warehouseId)
    {
        return this.applicationDbContext.WarehouseProducts.FirstOrDefault(x => x.WarehouseId == warehouseId && x.WarehouseProductId == productId);
    }

    public List<WarehouseProducts> GetByWarehouseId(int warehouseId)
    {
        return this.applicationDbContext.WarehouseProducts
            .AsNoTracking()
            .Include(x => x.WarehouseProduct)
            .Include(x => x.Warehouse)
            .Where(x => x.WarehouseId == warehouseId)
            .ToList();
    }

    public bool ReorderQuantity(int? warehouseId, int productId, int quantity)
    {
        var wp = this.GetByProductIdAndWarehouseId(productId, warehouseId);

        if (wp == null) return false;

        wp.QuantityInStock += quantity;
        this.Update(wp);

        return true;
    }

    public void Update(WarehouseProducts entity)
    {
        applicationDbContext.Update(entity).Property(x => x.Id).IsModified = false;
        applicationDbContext.SaveChanges();
    }

    public bool UpdateQuantity(int? warehouseId, int productId, int quantity)
    {
        var wp = this.GetByProductIdAndWarehouseId(productId, warehouseId);
        if (wp == null) return false;

        if (wp.QuantityInStock > quantity)
        {
            wp.QuantityInStock -= quantity;

            this.Update(wp);
            return true;
        }
        
        return false;
    }
}
