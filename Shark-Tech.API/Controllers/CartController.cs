using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shark_Tech.DAL;
using Shark_Tech.DAL.Models.CustomerCart;

namespace Shark_Tech.API.Controllers
{

    public class CartController : BaseController
    {
        public CartController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {

        }
        [HttpGet("GetCartItems/{Id}")]
        public async Task<IActionResult> GetCartItems(int Id)
        {
            var cartItems = await unitOfWork.CustomerCartRepository.GetCartAsync(Id);
            if (cartItems == null)
            {
                return Ok(new CustomerCart());
            }
            return Ok(cartItems);
        }

        [HttpPost("UpdateCartItems")]
        public async Task<IActionResult> UpdateCartItems([FromBody] CustomerCart cart)
        {
            if (cart == null || cart.Id <= 0)
            {
                return BadRequest("Invalid cart data.");
            }
            var updatedCart = await unitOfWork.CustomerCartRepository.UpdateCartAsync(cart);
            if (updatedCart == null)
            {
                return NotFound("Cart not found or update failed.");
            }
            return Ok(updatedCart);
        }

        [HttpDelete("DeleteCart/{Id}")]
        public async Task<IActionResult> DeleteCart(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid  ID.");
            }
            var result = await unitOfWork.CustomerCartRepository.DeleteCartAsync(Id);
            if (!result)
            {
                return NotFound("Cart not found or deletion failed.");
            }
            return Ok("Cart deleted successfully.");
        }

    }
}
