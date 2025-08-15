using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext, IBus bus)
{
    public async Task<IEnumerable<Product>> GetProductAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }
    
    public async Task CreateProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product updateProduct, Product inputProduct)
    {
        if (updateProduct.Price != inputProduct.Price)
        {
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = updateProduct.Id,
                Name = inputProduct.Name,
                Description = inputProduct.Description,
                Price = inputProduct.Price,
                ImageUrl = inputProduct.ImageUrl,
            };

            await bus.Publish(integrationEvent);
        }
        
        updateProduct.Name = inputProduct.Name;
        updateProduct.Description = inputProduct.Description;
        updateProduct.Price = inputProduct.Price;
        updateProduct.ImageUrl = inputProduct.ImageUrl;

        dbContext.Products.Update(updateProduct);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product product)
    {
        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        return await dbContext.Products
            .Where(x => x.Name.Contains(query))
            .ToListAsync();
    }
}