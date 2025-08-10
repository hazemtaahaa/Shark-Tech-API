namespace Shark_Tech.DAL.Models.CustomerCart
{
    public class CartItem
    {
        public Guid Id { get; set; }

        public string ProductName { get; set; }

        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;
        // Additional properties can be added as needed'


        public string Name { get; set; }
        public string? Description { get; set; }


      
        public virtual ProductImage ProductImage { get; set; }


    }
}