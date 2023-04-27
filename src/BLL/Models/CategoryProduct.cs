﻿namespace BLL.Models
{
    public class CategoryProduct
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public Category? Category { get; set; }
    }
}
