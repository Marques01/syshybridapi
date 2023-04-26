using BLL.Models;
using BLL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
	[Route("api/[controller]")]
	//[Authorize(Roles = "Admin")]
	public class DevicesController : ControllerBase
	{
		private readonly IUnitOfWork _uof;

		public DevicesController(IUnitOfWork uof)
		{
			_uof = uof;
		}

		[HttpGet]
		public async Task<ActionResult> GetDevices()
		{
			var devices = await _uof.DevicesRepository.GetDevicesAsync();

			return Ok(devices);
		}

		[HttpPost]
		[Route("associate/userdevice")]
		public async Task<ActionResult> AssociateUserDevice([FromQuery] int userId, [FromQuery] int deviceId)
		{
			await _uof.DevicesRepository.AssociateUserDeviceAsync(userId, deviceId);

			await _uof.CommitAsync();

			return Ok("success");
		}

		[HttpPost]
		[Route("associate/useralldevices")]
		public async Task<ActionResult> AssociateuserAllDevices([FromQuery] int userId)
		{
			await _uof.DevicesRepository.AssociateUserAllDevicesAsync(userId);

			await _uof.CommitAsync();

			return Ok();
		}

		[HttpPost]
		[Route("registerdevices")]
		public async Task<ActionResult> RegisterDevice([FromBody] IEnumerable<Devices> devices)
		{
            await _uof.DevicesRepository.CreateAsync(devices);

			await _uof.CommitAsync();

			return Ok("success");
		}
	}
}
