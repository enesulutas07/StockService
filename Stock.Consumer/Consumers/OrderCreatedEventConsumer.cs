using ECommerce.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Stock.Business;

namespace Stock.Consumer.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IStockService _stockService;
        private readonly ILogger<OrderCreatedEventConsumer> _logger;

        public OrderCreatedEventConsumer(IStockService stockService, ILogger<OrderCreatedEventConsumer> logger)
        {
            _stockService = stockService;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var message = context.Message;

            if (message.Products is not { Count: > 0 })
                return;

            foreach (var item in message.Products)
            {
                try
                {
                    await _stockService.DeductStockAsync(item.ProductId, item.Quantity, context.CancellationToken);
                    _logger.LogInformation(
                        "Stok düşüldü - OrderId: {OrderId}, ProductId: {ProductId}, Miktar: {Quantity}",
                        message.OrderId, item.ProductId, item.Quantity);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Stok düşümü başarısız - OrderId: {OrderId}, ProductId: {ProductId}, CorrelationId: {CorrelationId}",
                        message.OrderId, item.ProductId, context.CorrelationId);
                    throw;
                }
            }
        }
    }
}
