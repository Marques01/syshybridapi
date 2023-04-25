namespace BLL.Models
{
    public class Devices
    {
        public int DeviceId { get; set; }
        
        public string Mac { get; set; } = string.Empty;
        
        public int UserId { get; set; }
        
        public User? User { get; set; }
    }
}
