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
        [Required]
        public double Price { get; set; }
        [Required]
        public int SellerId { get; set; }
        [Required, MinLength(2)]
        public string Brand { get; set; }
    }
}
