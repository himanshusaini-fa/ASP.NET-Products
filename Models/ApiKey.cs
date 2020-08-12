using System.ComponentModel.DataAnnotations;

namespace Products.Models
{
    public class ApiKey
    {
        [Key]
        public int id;
        [Key, Required, MinLength(16)]
        public string key;
    }
}