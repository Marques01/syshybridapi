using BLL.Models;

namespace BLL.Repository.Interfaces
{
	public interface IDevicesRepository
    {
        Task CreateAsync(Devices devices);

		Task CreateAsync(IEnumerable<Devices> devices);

		Task UpdateAsync(Devices device);

        Task DeleteAsync(int id);

        Task AssociateUserDeviceAsync(int userId, int deviceId);

        Task AssociateUserAllDevicesAsync(int userId);        

        Task<Devices> GetDeviceByMACAsync(string mac);

		Task<Devices> GetDeviceByMACAsync(IEnumerable<string> macs);

		Task<Devices> GetDeviceByIdAsync(int id);

        Task<IEnumerable<Devices>> GetDevicesAsync();

        Task<IEnumerable<UserDevices>> GetDevicesByUserIdAsync(int userId);

        Task<bool> VerifyDeviceAuthorizedAsync(IEnumerable<string> macs, int userId);
    }
}
