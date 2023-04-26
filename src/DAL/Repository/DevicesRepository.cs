using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Utils.Logger;

namespace DAL.Repository
{
    public class DevicesRepository : IDevicesRepository
    {
        private readonly ApplicationDbContext _context;

        public DevicesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AssociateUserAllDevicesAsync(int userId)
        {
            try
            {
                string query =
                                @"SELECT @USERID, D.DEVICEID, D.MAC, D.Description
	                              FROM DEVICES D
	                              LEFT JOIN USERDEVICES UD 
                                  ON D.DEVICEID = UD.DEVICEID AND UD.USERID = @USERID
	                              WHERE UD.ID IS NULL;";

                var devicesNotAssociated = await _context.Devices.FromSqlRaw(query, new MySqlParameter("@USERID", userId)).ToListAsync();

                List<UserDevices> userDevices = new();


                foreach (var device in devicesNotAssociated)
                {
                    userDevices.Add(new UserDevices()
                    {
                        DeviceId = device.DeviceId,
                        UserId = userId
                    });
                }

                if (userDevices.Count > 0)
                    await _context.UserDevices.AddRangeAsync(userDevices);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível associar o usuário aos dispositivos\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task AssociateUserDeviceAsync(int userId, int deviceId)
        {
            try
            {
                var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.UserId.Equals(userId));

                var device = await GetDeviceByIdAsync(deviceId);

                if (user is not null && device.DeviceId > 0)
                {
                    UserDevices userDevices = new()
                    {
                        UserId = user.UserId,
                        DeviceId = deviceId
                    };

                    await _context.AddAsync(userDevices);
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível associar o usuário ao dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task CreateAsync(Devices devices)
        {
            try
            {
                await _context.Devices.AddAsync(devices);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível cadastrar o dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task CreateAsync(IEnumerable<Devices> devices)
        {
            try
            {
                await _context.Devices.AddRangeAsync(devices);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível cadastrar o dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var device = await GetDeviceByIdAsync(id);

                _context.Devices.Remove(device);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível deletar o dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<IEnumerable<Devices>> GetDevicesAsync()
        {
            try
            {
                var devices = await _context.Devices.AsNoTracking().ToListAsync();

                if (devices is not null)
                    return devices;

                return Enumerable.Empty<Devices>();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar os dispositivos\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Devices> GetDeviceByIdAsync(int id)
        {
            try
            {
                var device = await _context.Devices.FirstOrDefaultAsync(x => x.DeviceId.Equals(id));

                if (device is not null)
                    return device;

                return new Devices();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o dispositivo pelo id\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Devices> GetDeviceByMACAsync(string mac)
        {
            try
            {
                var device = await _context.Devices.AsNoTracking().FirstOrDefaultAsync(x => x.Mac.Equals(mac));

                if (device is not null)
                    return device;

                return new Devices();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o dispositivo pelo mac\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<Devices> GetDeviceByMACAsync(IEnumerable<string> macs)
        {
            try
            {
                foreach (var mac in macs)
                {
                    var device = await _context.Devices.AsNoTracking().Include(x => x.UserDevices).FirstOrDefaultAsync(x => x.Mac.Equals(mac));

                    if (device is not null)
                        return device;
                }

                return new Devices();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o dispositivo pelo mac\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<IEnumerable<UserDevices>> GetDevicesByUserIdAsync(int userId)
        {
            try
            {
                var devices = await _context.UserDevices
                    .AsNoTracking()
                    .Include(x => x.User)
                    .Include(x => x.Devices)
                    .Where(u => u.UserId.Equals(userId))
                    .ToListAsync();

                if (devices is not null)
                    return devices;

                return Enumerable.Empty<UserDevices>();
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível buscar o dispositivo pelo id do usuário\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task UpdateAsync(Devices device)
        {
            try
            {
                _context.Devices.Update(device);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível atualizar o dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }
    }
}
