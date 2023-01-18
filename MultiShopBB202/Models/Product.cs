namespace MultiShopBB202.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public bool IsDeleted { get; set; }
        public string Information { get; set; }
        public int? DiscountId { get; set; }
        public int CategoryId { get; set; }
        public Discount Discount { get; set;}
        public Category Category { get; set; }
        public List<ProductSize> ProductSizes { get; set; }
        public List<ProductColor> ProductColors { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<Product> Products { get; set; }
    }
    
}
