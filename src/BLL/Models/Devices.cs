namespace BLL.Models
{
	public class Devices
	{
		public int DeviceId { get; set; }

		public string Mac { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public ICollection<UserDevices>? UserDevices { get; set; }
	}
}
