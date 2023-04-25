using BLL.Repository.Interfaces;
using DAL.Context;
using Utils.Logger;

namespace DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IUserRepository UserRepository { get; }

        public IUserRolesRepository UserRoleRepository { get; }

        public IRolesRepository RoleRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IDevicesRepository DevicesRepository { get; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            UserRepository = new UserRepository(_context);

            UserRoleRepository = new UserRolesRepository(_context);

            RoleRepository = new RolesRepository(_context);

            ProductRepository = new ProductRepository(_context);

            DevicesRepository = new DevicesRepository(_context);
        }
        
        public async Task<bool> CommitAsync()
        {
            try
            {
                return Convert.ToBoolean(await _context.SaveChangesAsync());
            }
            catch (Exception ex)
            {
                string errorMessage = "Falha ao salvar as alterações\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task DisposeAsync()
        {
            try
            {
                await _context.DisposeAsync();
            }
            catch (Exception ex)
            {
                string errorMessage = "Falha ao liberar recursos do database\t";

                await RegisterLogs.CreateAsync(errorMessage += ex.Message, this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
