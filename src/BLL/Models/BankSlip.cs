namespace BLL.Models
{
    public class BankSlip
    {
        public int BankSlipId { get; set; }

        public int PurchaseId { get; set; }

        public DateTime PaymentDate { get; set; }

        public string Status { get; set; } = string.Empty;
    }
}
