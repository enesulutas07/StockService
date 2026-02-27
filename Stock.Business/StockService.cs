using Stock.Data;

namespace Stock.Business
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default)
        {
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

            var stock = await _stockRepository.GetByProductIdAsync(productId, cancellationToken)
                ?? throw new InvalidOperationException($"Ürün stok kaydı bulunamadı: ProductId={productId}");

            if (stock.Quantity < quantity)
                throw new InvalidOperationException(
                    $"Yetersiz stok. ProductId={productId}, Mevcut={stock.Quantity}, İstenen={quantity}");

            stock.Quantity -= quantity;
            stock.LastUpdated = DateTime.UtcNow;

            await _stockRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
