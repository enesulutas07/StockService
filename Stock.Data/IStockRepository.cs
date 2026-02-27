using Stock.Entity;

namespace Stock.Data
{
    public interface IStockRepository
    {
        Task<ProductStock?> GetByProductIdAsync(int productId, CancellationToken cancellationToken = default);
        Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default);
        Task AddOrUpdateStockAsync(int productId, int quantity, CancellationToken cancellationToken = default);
    }
}
