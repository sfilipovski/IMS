using IMS.Domain.Models;
using IMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace IMS.Repository.Implementation;

public class ProductRepository : IRepository<Product>
{
    private readonly ApplicationDbContext applicationDbContext;

    public ProductRepository(ApplicationDbContext applicationDbContext)
    {
        this.applicationDbContext = applicationDbContext;
    }

    public void Create(Product product)
    {
        //product.ProductCategory = applicationDbContext.Categories.FirstOrDefault(x => x.Id == product.ProductCategoryId);
        applicationDbContext.Products.Add(product);
        applicationDbContext.SaveChanges();
    }

    public void Delete(Product entity)
    {
        applicationDbContext.Remove(entity);
        applicationDbContext.SaveChanges();
    }

    public Product Get(int? id)
    {
        return applicationDbContext.Products.FirstOrDefault(x => x.Id == id);
    }

    public ICollection<Product> GetAll()
    {
        return applicationDbContext.Products
            .AsNoTracking()
            .Include(x => x.ProductCategory)
            .Include(x => x.ProductSupplier)
            .ToList();
    }


    public void Update(Product entity)
    {
        //entity.ProductCategory = applicationDbContext.Categories.FirstOrDefault(x => x.Id == entity.ProductCategoryId);
        applicationDbContext.Update(entity).Property(x => x.Id).IsModified = false;
        applicationDbContext.SaveChanges();
    }
}
