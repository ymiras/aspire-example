namespace Catalog.Endpoints;

public static class ProductEndpoints
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/products");

        // Get all.
        group.MapGet("/", async (ProductService service) =>
        {
            var products = await service.GetProductAsync();
            return Results.Ok(products);
        })
        .WithName("GetAllProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);
        
        // Get by ID.
        group.MapGet("/{id}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);
            if (product is null)
                return Results.NotFound();
            
            return Results.Ok(product);
        })
        .WithName("GetProductById")
        .Produces<Product>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
        
        // POST (Create)
        group.MapPost("/", async (Product product, ProductService service) =>
        {
            await service.CreateProductAsync(product);
            return Results.Created($"/products/{product.Id}", product);
        })
        .WithName("CreateProduct")
        .Produces<Product>(StatusCodes.Status201Created);
        
        // PUT (Update)
        group.MapPut("/{id}", async (int id, Product inputProduct, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);
            if (product is null) return Results.NotFound();

            await service.UpdateProductAsync(product, inputProduct);
            return Results.NoContent();
        })
        .WithName("UpdateProduct")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
        
        // DELETE
        group.MapDelete("/{id}", async (int id, ProductService service) =>
        {
            var product = await service.GetProductByIdAsync(id);
            if (product is null) return Results.NotFound();

            await service.DeleteProductAsync(product);
            return Results.NoContent();
        })
        .WithName("DeleteProduct")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        // Support AI.
        group.MapGet("/support/{query}", async (string query, ProductAiService service) =>
        {
            var response = await service.SupportAsync(query);

            return Results.Ok(response);
        })
        .WithName("Support")
        .Produces(StatusCodes.Status200OK);
        
        // Search
        group.MapGet("/search/{query}", async (string query, ProductService service) =>
        {
            var products = await service.SearchProductsAsync(query);

            return Results.Ok(products);
        })
        .WithName("SearchProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);

        group.MapGet("/aisearch/{query}", async (string query, ProductAiService service) =>
        {
            var products = await service.SearchProductsAsync(query);

            return Results.Ok(products);
        })
        .WithName("AiSearchProducts")
        .Produces<List<Product>>(StatusCodes.Status200OK);
    }
}