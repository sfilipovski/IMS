using IMS.Domain.DTO.Command;
using IMS.Domain.DTO.Query;
using IMS.Domain.Models;

namespace IMS.Service.Interface;

public interface ICategoryService
{
    List<Category> GetAllCategories();
    Category GetCategoryById(int? id);
    void CreateNewCategory(Category category);
    void UpdateCategory(int id, Category category);
    void DeleteCategory(int? id);
}
