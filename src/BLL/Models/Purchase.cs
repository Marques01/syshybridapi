namespace BLL.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }

        public int SupplierId { get; set; }

        public string Invoice { get; set; } = string.Empty;

        public decimal Value { get; set; }

        public string Status { get; set; } = string.Empty;        

        public DateTime PurchaseDate { get; set; }

        public DateTime DueDate { get; set; }

        public Supplier? Supplier { get; set; }
    }
}
