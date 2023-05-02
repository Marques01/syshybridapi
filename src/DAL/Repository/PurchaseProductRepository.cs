using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Utils.Logger;

namespace DAL.Repository
{
	public class PurchaseProductRepository : IPurchaseProductRepository
	{
		private readonly ApplicationDbContext _context;

        public PurchaseProductRepository(ApplicationDbContext context)
        {
			_context = context;
        }

        public async Task AddProductAsync(PurchaseProduct purchaseProduct)
		{
			try
			{
				await _context.PurchaseProducts.AddAsync(purchaseProduct);
			}
			catch (Exception ex)
			{
				string errorMessage = "Não foi possível adicionar o produto a lista de compra\n";

				await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public Task DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task UpdateProductAsync(PurchaseProduct purchaseProduct)
		{
			throw new NotImplementedException();
		}	
	}
}
