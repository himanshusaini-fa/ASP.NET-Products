using System;
using System.ComponentModel.DataAnnotations;

namespace Products.Dtos
{
    public class ProductUpdateDto
    {
        [MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [MinLength(3)]
        public string Category { get; set; }
        [Range(0, double.MaxValue)]
        public double Price { get; set; }
        [Range(0, int.MaxValue)]
        public int SellerId { get; set; }
        [MinLength(2)]
        public string Brand { get; set; }
    }
}
