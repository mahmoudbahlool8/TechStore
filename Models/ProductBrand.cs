using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class ProductBrand
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
    }
}
