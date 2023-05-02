using BLL.Models;

namespace BLL.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Task CreateAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int id);

        Task<Category> GetByIdAsync(int id);

        Task<Category> GetByNameAsync(string name);

        Task<IEnumerable<Category>> GetCategoriesAsync();
    }
}
