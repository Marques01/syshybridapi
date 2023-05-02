using BLL.Models;
using BLL.Repository.Interfaces;

namespace DAL.Repository
{
	public class SupplierRepository : ISupplierRepository
	{
		public Task CreateAsync(Supplier supplier)
		{
			throw new NotImplementedException();
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}		

		public Task UpdateAsync(Supplier supplier)
		{
			throw new NotImplementedException();
		}

		public Task<Supplier> GetSupplierByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
