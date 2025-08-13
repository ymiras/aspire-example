using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.Services;

public class BasketService(IDistributedCache cache, CatalogApiClient catalogApiClient)
{
    public async Task<ShoppingCart?> GetBasket(string username)
    {
        var basket = await cache.GetStringAsync(username);

        return string.IsNullOrEmpty(basket) 
            ? null 
            : JsonSerializer.Deserialize<ShoppingCart>(basket);
    }

    public async Task UpdateBasket(ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            var product = await catalogApiClient.GetProductByIdAsync(item.ProductId);
            item.Price = product.Price;
            item.ProductName = product.Name;
        }
        
        await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));
    }

    public async Task DeleteBasket(string username)
    {
        await cache.RemoveAsync(username);
    }

    public async Task UpdateBasketItemProductPrices(int productId, decimal price)
    {
        // IDistributedCache note supported list of keys function.
        // https://github.com/dotnet/runtime/issues/36402
        
        var basket = await GetBasket("ymiras");
        var item = basket!.Items.FirstOrDefault(x => x.ProductId == productId);
        if (item != null)
        {
            item.Price = price;
            await cache.SetStringAsync(basket.Username, JsonSerializer.Serialize(basket));
        }
    }
}