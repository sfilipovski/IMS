namespace IMS.Domain.DTO.Command;

public class ReorderQuantityDto
{
    public int Id { get; set; }
    public int WarehouseId { get; set; }
    public int ProductId { get; set; }
    public int ReorderQuantity { get; set; } 
   
}
