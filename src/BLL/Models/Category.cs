namespace BLL.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;
        
        public ICollection<CategoryProduct>? CategoriesProducts { get; set; }
    }
}
