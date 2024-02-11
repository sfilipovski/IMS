using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;
using IMS.Repository;
using IMS.Repository.Interface;
using IMS.Service.Interface;

namespace IMS.Service.Implementation;

public class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;

    public CategoryService(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void CreateNewCategory(Category category)
    {
        this._categoryRepository.Create(category);
    }

    public void DeleteCategory(int? id)
    {
        var category = _categoryRepository.Get(id);
        _categoryRepository.Delete(category);
    }

    public List<Category> GetAllCategories()
    {
        return this._categoryRepository.GetAll().ToList();
    }

    public Category GetCategoryById(int? id)
    {
        return this._categoryRepository.Get(id);
    }

    public void UpdateCategory(int id, Category update)
    {
        var category = this._categoryRepository.Get(id);
        category.CategoryName = update.CategoryName;
        category.CategoryDescription = update.CategoryDescription;
        category.CategoryType = update.CategoryType;
        this._categoryRepository.Update(category);
    }
}
