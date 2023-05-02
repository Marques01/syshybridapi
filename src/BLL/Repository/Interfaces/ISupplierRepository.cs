using BLL.Models;

namespace BLL.Repository.Interfaces
{
	public interface ISupplierRepository
	{
		Task CreateAsync(Supplier supplier);

		Task UpdateAsync(Supplier supplier);

		Task DeleteAsync(int id);

		Task<Supplier> GetSupplierByIdAsync(int id);
	}
}
