namespace IMS.Domain.DTO.Command;

public class CreateProductDto
{
    public string ProductName { get; set; }
    public string? ProductDescription { get; set; }
    public string? ProductSKU { get; set; }
    public string? ProductImageUrl { get; set; }
    public double ProductPrice { get; set; }
    public int? CategoryId { get; set; }
    public int? SupplierId { get; set; }
}
