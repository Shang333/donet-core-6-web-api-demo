using System.ComponentModel.DataAnnotations;

namespace Client.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }
        [Required]
        [Range(10, 10000, ErrorMessage = "Price should be between 10 to 10000 only")]
        public decimal Price { get; set; }
    }
}
