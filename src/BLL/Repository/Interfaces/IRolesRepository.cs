using BLL.Models;

namespace BLL.Repository.Interfaces
{
    public interface IRolesRepository
    {
        Task CreateAsync(Roles roles);
    }
}
