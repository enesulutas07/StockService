using Stock.Entity;

namespace Stock.Data
{
    public interface IStockRepository
    {
        Task<ProductStock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
