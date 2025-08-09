using Shark_Tech.DAL.Models.CustomerCart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shark_Tech.DAL
{
    public interface ICustomerCartRepository
    {
        Task<CustomerCart> GetCartAsync(int Id);
        Task<CustomerCart> UpdateCartAsync( CustomerCart cart);

        Task<bool> DeleteCartAsync(int Id);
    }
}
