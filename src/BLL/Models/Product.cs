namespace BLL.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public decimal UnitaryValue { get; set; }               

        public string BarCode { get; set; } = string.Empty;

        public string PathImage { get; set; } = string.Empty;

        public Stock? Stock { get; set; }

        public ICollection<CategoryProduct>? CategoriesProducts { get; set; }

        public ICollection<PurchaseProduct>? PurchaseProducts { get; set; }
    }
}
