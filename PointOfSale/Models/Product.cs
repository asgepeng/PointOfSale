using System.Collections.Generic;

namespace PointOfSale.Models
{
    public class Product
    {
        public int Id { get; set; } = 0;
        public string Sku { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Category { get; set; } = 0;
        public decimal Stock { get; set; } = 0;
        public string Unit { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public decimal BasicPrice { get; set; } = 0;
        public decimal Margin => Price - BasicPrice;
        public int Supplier { get; set; } = 0;
        public List<string> Images { get; } = new List<string>();
    }

    public class ProductCategory
    {
        public int Id { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
    }

    public class ProductView
    {
        public Product Product { get; } = new Product();
        public List<Category> Categories { get; } = new List<Category>();
        public List<string> Units { get; } = new List<string>();
    }
}
