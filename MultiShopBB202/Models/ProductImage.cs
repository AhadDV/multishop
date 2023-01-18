namespace MultiShopBB202.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string Image { get; set; }

        public bool IsMain { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductId { get; set; }

    }
}
