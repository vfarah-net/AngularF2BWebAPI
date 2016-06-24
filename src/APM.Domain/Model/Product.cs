using System;

namespace APM.Domain.Model
{
    public class Product
    {
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
