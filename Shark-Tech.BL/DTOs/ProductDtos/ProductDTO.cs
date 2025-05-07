

using Microsoft.AspNetCore.Http;

namespace Shark_Tech.BL;

public record ProductDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal OldPrice { get; set; }
    public decimal NewPrice { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Quantity { get; set; } = 0;
    public virtual List<ProductImageDTO> ProductImages { get; set; }
    public string CategoryName { get; set; }


}

public record ProductImageDTO
{
    public string ImageUrl { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
}

public record AddProductDTO
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; }
    public Guid CategoryId { get; set; }
    public int Quantity { get; set; } = 0;
    public IFormFileCollection ProductImages { get; set; }
}
public record UpdateProductDTO
{
    public Guid Id { get; set; } 
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; }
    public Guid CategoryId { get; set; }
    public int Quantity { get; set; } = 0;
    public IFormFileCollection ProductImages { get; set; }
}