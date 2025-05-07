using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL;

public class Product: BaseEntity<Guid>
{
    [Required(ErrorMessage = "Name is Required")]
    public string  Name { get; set; }
    public string? Description { get; set; }
    public decimal NewPrice { get; set; }
    public decimal OldPrice { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public int Quantity { get; set; } = 0;
    public virtual List<ProductImage> ProductImages { get; set; } 

    public Guid CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; }
    
}
