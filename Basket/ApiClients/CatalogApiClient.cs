// using Basket.Models;

using Catalog.Models;

namespace Basket.ApiClients;

public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"products/{productId}");
        
        return response;
    }
}