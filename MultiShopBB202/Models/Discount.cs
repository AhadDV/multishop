namespace MultiShopBB202.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public double Percent { get; set;}
        public bool IsDeleted { get; set; }
        public List<Product> Products { get; set; }
    }
}
