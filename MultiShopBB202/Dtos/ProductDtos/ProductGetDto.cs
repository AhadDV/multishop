namespace MultiShopBB202.Dtos.ProductDtos
{
    public class ProductGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool InStock { get; set; }
        public string Information { get; set; }
        public int? DiscountId { get; set; }
        public int CategoryId { get; set; }
        public List<string> ProductImages { get; set; }
        public List<int> SizeIds { get; set; }
        public List<int> ColorIds { get; set; }
        public List<int> Counts { get; set; }
    }
}
