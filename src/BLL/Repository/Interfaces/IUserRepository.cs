using BLL.Models;

namespace BLL.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);

        Task UpdateAsync(User user);

        Task<bool> UserExistingAsync(User user);

        Task<User> GetUserByIdAsync(int id);        

        Task<BaseModel> SignInAsync(UserDto user);

        Task<User> GetUserByMailAsync(string mail);
    }
}
