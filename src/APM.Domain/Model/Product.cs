using System;
using System.ComponentModel.DataAnnotations;

namespace APM.Domain.Model
{
    public class Product
    {
        public Product()
        {
            ReleaseDate = DateTime.Now;
        }

        public string Description { get; set; }
        [Required(), MinLength(4), MaxLength(12)]
        public string ProductName { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required, MinLength(6)]
        public string ProductCode { get; set; }
        public int ProductId { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
