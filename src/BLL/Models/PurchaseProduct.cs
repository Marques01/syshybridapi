namespace BLL.Models
{
    public class PurchaseProduct
    {
        public int PurchaseProductId { get; set; }

        public int PurchaseId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitaryValue { get; set; }
    }
}
