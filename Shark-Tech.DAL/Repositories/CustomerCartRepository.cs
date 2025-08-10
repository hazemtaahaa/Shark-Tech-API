
using Microsoft.IdentityModel.Tokens;
using Shark_Tech.DAL.Models.CustomerCart;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shark_Tech.DAL
{
    public class CustomerCartRepository : ICustomerCartRepository
    {
        private readonly IDatabase _database;
        public CustomerCartRepository(IConnectionMultiplexer redis)
        {
                _database = redis.GetDatabase();
        }
        public Task<bool> DeleteCartAsync(Guid Id)
        {
           return _database.KeyDeleteAsync($"{Id}");
        }

        public async Task<CustomerCart> GetCartAsync(Guid Id)
        {
            var result = await _database.StringGetAsync($"{Id}");
            if(!string.IsNullOrEmpty(result))
            {
                return JsonSerializer.Deserialize<CustomerCart>(result);

            }
            return null;
        }

        public async Task<CustomerCart> UpdateCartAsync(CustomerCart cart)
        {
            var _cart = await _database.StringSetAsync($"{cart.Id}", JsonSerializer.Serialize(cart), TimeSpan.FromDays(30));
            if(_cart)
            {
                return await GetCartAsync(cart.Id);
            }
            return null;
        }
    }
}
