using BLL.Repository.Interfaces;
using DAL.Repository;

namespace API.Configurations
{
	public class DependencyInjection
    {
        public static void RegisterConfiguration(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRolesRepository, UserRolesRepository>();
            services.AddScoped<IRolesRepository, RolesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IDevicesRepository, DevicesRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
			services.AddScoped<IPurchaseProductRepository, PurchaseProductRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
