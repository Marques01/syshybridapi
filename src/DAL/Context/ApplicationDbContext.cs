using BLL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Roles> Roles { get; set; }

        public DbSet<UserRoles> UserRoles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryProduct> CategoriesProducts { get; set; }

        public DbSet<Devices> Devices { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :base(options) 
        {
            Users = Set<User>();

            Roles = Set<Roles>();

            UserRoles = Set<UserRoles>();

            Products = Set<Product>();

            Categories = Set<Category>();

            CategoriesProducts = Set<CategoryProduct>();

            Devices = Set<Devices>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;

            base.OnModelCreating(modelBuilder);
        }
    }
}