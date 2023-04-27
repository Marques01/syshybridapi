using BLL.Models;

namespace BLL.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);

        Task<Product> GetProductByIdAsync(int id);

		Task<Product> GetProductByBarCode(string barCode);

		Task<IEnumerable<Product>> GetProductByNameAsync(string name);

		Task<IEnumerable<Product>> GetProductsAsync();

        Task AssociateProductCategory(int productId, int categoryId);
    }
}
