namespace BLL.Models
{
    public class UserToken
    {
        public const string Sucess = "sucess";

		public const string AccessDenied = "Dispositivo não autorizado";

		public const string InvalidLogin = "Dispositivo não autorizado";

		public const string LimitExceeded = "Limite de tentativas excedida. Entre em contato com o administrador do sistema";

		public const string AccountExist = "Não é possível registrar uma conta existente";		

        public string Token { get; set; } = string.Empty;

        public DateTime Expiration { get; set; }

        public string Message { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;
    }
}
