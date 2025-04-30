using System.ComponentModel.DataAnnotations.Schema;

namespace Shark_Tech.DAL;

public class ProductImage : BaseEntity<Guid>
{
    public string ImageUrl { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public virtual Product Product { get; set; } 
}