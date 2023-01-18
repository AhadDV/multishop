namespace MultiShopBB202.Models
{
    public class Size
    {
        public int Id { get; set; }
        public string Value { get; set;}
        public bool IsDeleted { get; set; }
        public List<ProductSize> ProductSize { get; set; }
    }
}
