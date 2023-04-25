using BLL.Models;

namespace BLL.Repository.Interfaces
{
    public interface IUserRolesRepository
    {
        Task<UserRoles> GetUserRolesByIdAsync(int id);

        Task CreateAsync(UserRoles userRoles);

        Task DeleteAsync(int id);

        Task<List<Roles>> GetRolesByUserId(int id);
    }
}
