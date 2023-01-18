﻿namespace MultiShopBB202.Models
{
    public class ProductColor
    {
        public int Id { get; set; }
        public Color Color { get; set; }
        public Product Product { get; set; }
        public bool IsDeleted { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int Count { get; set; }


    }
}
