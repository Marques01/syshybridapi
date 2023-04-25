using BLL.Models;
using BLL.Repository.Interfaces;
using DAL.Context;
using Microsoft.EntityFrameworkCore;
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
                var device = await GetByIdAsync(id);

                _context.Devices.Remove(device);
            }
            catch (Exception ex)
            {
                string errorMessage = "Não foi possível deletar o dispositivo\t";

                await RegisterLogs.CreateAsync($"{errorMessage} {ex.Message}\t{ex.InnerException}\t{ex.StackTrace}", this.GetType().ToString());

                throw new Exception(errorMessage);
            }
        }

        public async Task<IEnumerable<Devices>> GetAllAsync()
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

        public async Task<Devices> GetByIdAsync(int id)
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

        public async Task<Devices> GetDeviceByMAC(string mac)
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

		public async Task<Devices> GetDeviceByMAC(IEnumerable<string> macs)
		{
			try
			{
                foreach (var mac in macs)
                {
                    var device = await _context.Devices.AsNoTracking().Include(x => x.User).FirstOrDefaultAsync(x => x.Mac.Equals(mac));

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

		public async Task<IEnumerable<Devices>> GetDevicesByUserId(int userId)
        {
            try
            {
                var devices = await _context.Devices.AsNoTracking().Where(x => x.UserId.Equals(userId)).ToListAsync();

                if (devices is not null)
                    return devices;

                return Enumerable.Empty<Devices>();
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
