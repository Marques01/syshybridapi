using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteProductAsync(int id)
        {
            try
            {
                var purchaseProducts = await GetPurchaseProductByIdAsync(id);

                _context.PurchaseProducts.Remove(purchaseProducts);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível deletar o produto da lista de compras\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }


        public Task UpdateProductAsync(PurchaseProduct purchaseProduct)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<PurchaseProduct>> GetByPurchaseIdAsync(int id)
        {
            try
            {
                var purchaseProducts = await _context.PurchaseProducts
                    .AsNoTracking()
                    .Include(u => u.User)
                    .Include(p => p.Product)                    
                    .Where(x => x.PurchaseId.Equals(id))
                    .ToListAsync();

                if (purchaseProducts is not null)
                    return purchaseProducts;

                return Enumerable.Empty<PurchaseProduct>();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar os produto das listas de compras\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<PurchaseProduct> GetPurchaseProductByIdAsync(int id)
        {
            try
            {
                var purchaseProducts = await _context.PurchaseProducts.FirstOrDefaultAsync(x => x.Id.Equals(id));

                if (purchaseProducts is not null)
                    return purchaseProducts;

                return new PurchaseProduct();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o produto da lista de compras\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
