using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required, MinLength(3)]
        public string Category { get; set; }
        [Required, Range(1, double.MaxValue)]
        public double Price { get; set; }
        [Required, Range(1, int.MaxValue)]
        public int SellerId { get; set; }
        [Required, MinLength(2)]
        public string Brand { get; set; }
    }
}
