using System.ComponentModel.DataAnnotations;

namespace Restaurant_5._0.Models
{
    public class CreateDishDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int RestaurantId { get; set; }

    }
}
