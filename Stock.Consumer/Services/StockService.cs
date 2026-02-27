using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Stock.Data;

namespace Stock.Consumer.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly ILogger<StockService> _logger;

        public StockService(IStockRepository stockRepository, ILogger<StockService> logger)
        {
            _stockRepository = stockRepository;
            _logger = logger;
        }

        public async Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default)
        {
            await _stockRepository.DeductStockAsync(productId, quantity, cancellationToken);
            _logger.LogInformation(
                "Stok servisi üzerinden düşüm yapıldı - ProductId: {ProductId}, Quantity: {Quantity}",
                productId, quantity);
        }
    }
}
