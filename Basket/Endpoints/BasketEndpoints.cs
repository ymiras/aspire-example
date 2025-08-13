namespace Basket.Endpoints;

public static class BasketEndpoints
{
    public static void MapBasketEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("basket");

        group.MapGet("/{username}", async (string username, BasketService service) =>
        {
            var shoppingCart = await service.GetBasket(username);
            if (shoppingCart is null) return Results.NotFound();

            return Results.Ok(shoppingCart);
        })
        .WithName("GetBasket")
        .Produces<ShoppingCart>(StatusCodes.Status200OK)
        .Produces<ShoppingCart>(StatusCodes.Status404NotFound);

        group.MapPost("/", async (ShoppingCart shoppingCart, BasketService service) =>
        {
            await service.UpdateBasket(shoppingCart);

            return Results.Created("GetBasket", shoppingCart);
        })
        .WithName("UpdateBasket")
        .Produces<ShoppingCart>(StatusCodes.Status201Created);

        group.MapDelete("/{username}", async (string username, BasketService service) =>
        {
            await service.DeleteBasket(username);

            return Results.NoContent();
        })
        .WithName("DeleteBasket")
        .Produces(StatusCodes.Status204NoContent);;
    }
}