using ECommerce.Shared.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stock.Consumer.Services;

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

            if (message.Products == null || message.Products.Count == 0)
                return;

            foreach (var item in message.Products)
            {
                try
                {
                    await _stockService.DeductStockAsync(item.ProductId, item.Quantity, context.CancellationToken);
                    _logger.LogInformation(
                        "Stok düşüldü - ProductId: {ProductId}, Düşülen miktar: {Quantity}, CorrelationId: {CorrelationId}",
                        item.ProductId, item.Quantity, context.CorrelationId);
                }
                catch (InvalidOperationException ex)
                {
                    _logger.LogWarning(
                        ex,
                        "Stok düşümü başarısız - ProductId: {ProductId}, CorrelationId: {CorrelationId}",
                        item.ProductId,
                        context.CorrelationId);
                    throw;
                }
            }
        }
    }
}
