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

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new InvalidOperationException(
                    "Stok güncellemesi sırasında eş zamanlı bir değişiklik algılandı. İşlem tekrar denenecek.", ex);
            }
        }
    }
}
