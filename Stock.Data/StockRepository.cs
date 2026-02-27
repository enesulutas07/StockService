using Microsoft.EntityFrameworkCore;
using Stock.Entity;

namespace Stock.Data
{
    public class StockRepository : IStockRepository
    {
        private readonly StockDbContext _context;

        public StockRepository(StockDbContext context)
        {
            _context = context;
        }

        public async Task<ProductStock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default)
        {
            return await _context.ProductStocks
                .FirstOrDefaultAsync(s => s.ProductId == productId, cancellationToken);
        }

        public async Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default)
        {
            var stock = await GetByProductIdAsync(productId, cancellationToken);
            if (stock == null)
                throw new InvalidOperationException($"Ürün stok kaydı bulunamadı: ProductId={productId}");

            if (stock.Quantity < quantity)
                throw new InvalidOperationException(
                    $"Yetersiz stok. ProductId={productId}, Mevcut={stock.Quantity}, İstenen={quantity}");

            stock.Quantity -= quantity;
            stock.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task AddOrUpdateStockAsync(int productId, int quantity, CancellationToken cancellationToken = default)
        {
            var stock = await GetByProductIdAsync(productId, cancellationToken);
            if (stock == null)
            {
                _context.ProductStocks.Add(new ProductStock
                {
                    ProductId = productId,
                    Quantity = quantity,
                    LastUpdated = DateTime.UtcNow
                });
            }
            else
            {
                stock.Quantity += quantity;
                stock.LastUpdated = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
