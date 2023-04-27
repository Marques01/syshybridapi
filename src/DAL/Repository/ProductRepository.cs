using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Utils.Logger;

namespace DAL.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

		public async Task AssociateProductCategory(int productId, int categoryId)
		{
            try
            {
                CategoryProduct categoryProduct = new()
                {
                    ProductId = productId,
                    CategoryId = categoryId,
                };

                await _context.CategoriesProducts.AddAsync(categoryProduct);
            }
            catch (Exception ex)
            {
				string errorMessage = "Não foi possível associar o produto a categoria\n";                

				await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task CreateAsync(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível criar o produto\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var product = await GetProductByIdAsync(id);

                _context.Products.Remove(product);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível deletar o produto\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Product> GetProductByBarCode(string barCode)
        {
            try
            {
                var product = await _context.Products.AsNoTracking().Include(x => x.CategoriesProducts).FirstOrDefaultAsync(x => x.BarCode == barCode);

                if (product is not null)
                    return product;

                return new Product();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o produto pelo código de barras\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _context.Products.Include(x => x.CategoriesProducts).FirstOrDefaultAsync(x => x.ProductId.Equals(id));

                if (product is not null)
                    return product;

                return new Product();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o produto pelo id\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

		public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
		{
            try
            {
                var product = await _context.Products.AsNoTracking().Include(x => x.CategoriesProducts).Where(x => x.Name.Contains(name)).ToListAsync();

                if (product is not null)
                    return product.AsEnumerable();

                return Enumerable.Empty<Product>();
            }
            catch (Exception ex)
            {
				string errorMessage = "Não foi possível buscar o produto pelo nome\t";

				await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

				throw new Exception(errorMessage);
			}
		}

		public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            try
            {
                var products = await _context.Products.AsNoTracking().Include(x => x.CategoriesProducts).ToListAsync();

                if (products is not null)
                    return products;

                return Enumerable.Empty<Product>();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar todos os produtos\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task UpdateAsync(Product product)
        {
            try
            {
                _context.Products.Update(product);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível atualizar o produto\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
