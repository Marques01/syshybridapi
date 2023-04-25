using BLL.Models;

namespace BLL.Repository.Interfaces
{
	public interface IDevicesRepository
    {
        Task CreateAsync(Devices devices);

		Task CreateAsync(IEnumerable<Devices> devices);

		Task UpdateAsync(Devices device);

        Task DeleteAsync(int id);

        Task<IEnumerable<Devices>> GetAllAsync();

        Task<IEnumerable<Devices>> GetDevicesByUserId(int userId);

        Task<Devices> GetDeviceByMAC(string mac);

		Task<Devices> GetDeviceByMAC(IEnumerable<string> mac);

		Task<Devices> GetByIdAsync(int id);
    }
}
