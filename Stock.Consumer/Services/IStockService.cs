using System.Threading;
using System.Threading.Tasks;

namespace Stock.Consumer.Services
{
    public interface IStockService
    {
        Task DeductStockAsync(int productId, int quantity, CancellationToken cancellationToken = default);
    }
}
