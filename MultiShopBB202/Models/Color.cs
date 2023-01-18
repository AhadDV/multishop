namespace MultiShopBB202.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Value { get; set;}
        public bool IsDeleted { get; set; }
        public List<ProductColor> ProductColor { get; set; }
    }
}
