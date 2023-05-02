using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Utils.Logger;

namespace DAL.Repository
{
	public class PurchaseRepository : IPurchaseRepository
	{
		private readonly ApplicationDbContext _context;

		public PurchaseRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task CreateAsync(Purchase purchase)
		{
			try
			{
				await _context.Purchases.AddAsync(purchase);
			}
			catch (Exception ex)
			{
				string errorMessage = "Não foi possível cadastrar a compra\n";

				await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public Task DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateAsync(Purchase purchase)
		{
			throw new NotImplementedException();
		}
	}
}
