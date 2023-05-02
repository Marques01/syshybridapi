namespace BLL.Models
{
    public class PurchaseProduct
    {
        public int Id { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitaryValue { get; set; }

        public User? User { get; set; }

        public Product? Product { get; set; }

        public Purchase? Purchase { get; set; }
    }
}
