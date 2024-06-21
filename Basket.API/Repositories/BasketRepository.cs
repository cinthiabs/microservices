using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache ?? throw new ArgumentException (nameof(cache));
        }

        public async Task DeleteBasket(string userName)
        {
            await _cache.RemoveAsync(userName);

        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket =  await _cache.GetStringAsync(userName);
            if(string.IsNullOrEmpty(basket))
            {
                return null;
            }
            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            await _cache.SetStringAsync(cart.UserName, JsonSerializer.Serialize(cart));
            return await GetBasket(cart.UserName);
        }
    }
}
