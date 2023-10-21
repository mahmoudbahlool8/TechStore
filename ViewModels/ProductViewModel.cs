using System.ComponentModel.DataAnnotations;
using TechStore.Models;

namespace TechStore.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Display(Name = "Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }
        public IEnumerable<ProductType>? productType { get; set; }

        [Display(Name = "Product Tag")]
        public int ProductTagId { get; set; }
        public IEnumerable<ProductTag>? productTag { get; set; }

        [Display(Name = "Product Brand")]
        public int ProductBrandId { get; set; }
        public IEnumerable<ProductBrand>? productBrand { get; set; }

    }
}
