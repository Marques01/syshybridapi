using BLL.Models;

namespace BLL.Repository.Interfaces
{
	public interface IPurchaseProductRepository
	{
		Task AddProductAsync(PurchaseProduct purchaseProduct);

		Task UpdateProductAsync(PurchaseProduct purchaseProduct);

		Task<IEnumerable<PurchaseProduct>> GetByPurchaseIdAsync(int id);

		Task DeleteProductAsync(int id);
	}
}
