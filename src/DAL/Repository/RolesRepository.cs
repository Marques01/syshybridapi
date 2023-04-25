using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Utils.Logger;

namespace DAL.Repository
{
    public class RolesRepository : IRolesRepository
    {
        private readonly ApplicationDbContext _context;

        public RolesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Roles roles)
        {
            try
            {
                await _context.Roles.AddAsync(roles);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível criar a Role\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
