namespace Stock.Business
{
    public interface IStockService
    {
        Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default);
    }
}
