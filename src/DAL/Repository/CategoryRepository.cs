using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Utils.Logger;

namespace DAL.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            try
            {
                await _context.Categories.AddAsync(category);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível cadastrar a categoria\n";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            try
            {
                var categories = await _context.Categories.AsNoTracking().ToListAsync();

                if (categories.Count > 0)
                    return categories;

                return Enumerable.Empty<Category>();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar todas as categorias\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public Task UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
