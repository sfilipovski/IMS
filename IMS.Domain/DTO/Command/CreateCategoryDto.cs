using System.ComponentModel.DataAnnotations;

namespace IMS.Domain.DTO.Command;

public class CreateCategoryDto
{
    public string CategoryName { get; set; }
    public string? CategoryType { get; set; }
    public string? CategoryDescription { get; set; }
}
