namespace BLL.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public IUserRolesRepository UserRoleRepository { get; }

        public IRolesRepository RoleRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IDevicesRepository DevicesRepository { get; }

        public ICategoryRepository CategoryRepository { get; }

		public IPurchaseRepository PurchaseRepository { get; }

		public ISupplierRepository SupplierRepository { get; }

        public IPurchaseProductRepository PurchaseProductRepository { get; }

		Task<bool> CommitAsync();

        Task DisposeAsync();
    }
}
