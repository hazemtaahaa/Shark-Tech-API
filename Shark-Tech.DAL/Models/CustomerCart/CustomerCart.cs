using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL.Models.CustomerCart
{
    public class CustomerCart
    {
        public CustomerCart()
        {
            
        }
        public CustomerCart(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        // Additional properties can be added as needed
    }
}
