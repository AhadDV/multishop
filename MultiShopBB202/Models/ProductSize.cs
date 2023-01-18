namespace MultiShopBB202.Models
{
    public class ProductSize
    {
        public int Id { get; set; }
        public Size Size { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public bool IsDeleted { get; set; }
        public int Count { get; set; }


    }
}
