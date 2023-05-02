using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Utils.Logger;

namespace DAL.Repository
{
	public class SupplierRepository : ISupplierRepository
	{
		private readonly ApplicationDbContext _context;

        public SupplierRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Supplier supplier)
		{
			try
			{
				await _context.Suppliers.AddAsync(supplier);
			}
			catch (Exception ex)
			{
				string errorMessage = "Não foi possível cadastrar o fornecedor\n";

				await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

				throw new Exception(errorMessage);
			}
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
