﻿using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Basket.EventHandlers;

public class ProductPriceChangedIntegrationEventHandler(BasketService service) 
    : IConsumer<ProductPriceChangedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<ProductPriceChangedIntegrationEvent> context)
    {
        await service.UpdateBasketItemProductPrices(
            context.Message.ProductId, context.Message.Price);
    }
}