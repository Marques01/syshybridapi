using BLL.Enum;
using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task DeleteAsync(int id)
        {
            try
            {
                var purchase = await GetPurchaseByIdAsync(id);

                _context.Purchases.Remove(purchase);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível deletar a compra\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task FinalizePurchaseAsync(Purchase purchase)
        {
            try
            {
                purchase.Value = _context.PurchaseProducts.Where(x => x.PurchaseId.Equals(purchase.PurchaseId)).Sum(x => (x.Quantity * x.UnitaryValue));

                purchase.Status = PurchaseStatus.Completed.ToString();

                _context.Purchases.Update(purchase); 
                
                var productsPurchases = await _context.PurchaseProducts.AsNoTracking().Where(x => x.PurchaseId.Equals(purchase.PurchaseId)).ToListAsync();

                await AddProductStockAsync(productsPurchases);
			}
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível finalizar a compra\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int purchaseId)
        {
            try
            {
                var purchase = await _context.Purchases.FirstOrDefaultAsync(x => x.PurchaseId.Equals(purchaseId));

                if (purchase is not null)
                    return purchase;

                return new Purchase();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar a compra pelo id\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task UpdateAsync(Purchase purchase)
        {
            try
            {
                _context.Purchases.Update(purchase);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar a compra pelo id\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        private async Task AddProductStockAsync(IEnumerable<PurchaseProduct> purchaseProducts)
        {
            List<Stock> stocks = new();

            foreach (var purchaseProduct in purchaseProducts)
            {
                stocks.Add(new Stock()
                {
                    Quantity = purchaseProduct.Quantity,
                    ProductId = purchaseProduct.ProductId,
                });
            }

            await _context.Stocks.AddRangeAsync(stocks);
        }
    }
}
