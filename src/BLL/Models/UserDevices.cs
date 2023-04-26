namespace BLL.Models
{
	public class UserDevices
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public int DeviceId { get; set; }

		public User? User { get; set; }

		public Devices? Devices { get; set; }
	}
}
