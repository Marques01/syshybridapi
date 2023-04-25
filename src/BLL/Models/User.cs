namespace BLL.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool Enabled { get; set; }

        public DateTime CreateAt { get; set; }

        public DateTime LastLogin { get; set; }

        public DateTime LastLogout { get; set; }

        public int FailedCount { get; set; }

        public ICollection<UserRoles>? UserRoles { get; set; }

        public ICollection<Devices>? Devices { get; set; }
    }
}