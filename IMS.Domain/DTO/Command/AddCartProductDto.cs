
using IMS.Domain.Models;

namespace IMS.Domain.DTO.Command;

public class AddCartProductDto
{
    public int ProductId { get; set; }
    public int? WarehouseId { get; set; }
    public Product? CartProduct { get; set; }
    public int Quantity { get; set; }
}
