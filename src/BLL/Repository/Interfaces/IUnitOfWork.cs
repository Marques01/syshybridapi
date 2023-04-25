namespace BLL.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public IUserRolesRepository UserRoleRepository { get; }

        public IRolesRepository RoleRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IDevicesRepository DevicesRepository { get; }

        Task<bool> CommitAsync();

        Task DisposeAsync();
    }
}
