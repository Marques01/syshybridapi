namespace BLL.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CNPJ { get; set; } = string.Empty; 

        public string CorporateName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;
    }
}
