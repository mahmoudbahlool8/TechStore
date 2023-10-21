using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TechStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string? Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Display(Name ="Available")]
        public bool IsAvailable { get; set; }
        
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }
        //[ForeignKey("ProductTypeId")]
        public virtual ProductType? ProductType { get; set; }

        [Display(Name = "Product Tag")]
        public int ProductTagId { get; set; }
        //[ForeignKey("ProductTagId")]
        public virtual ProductTag? ProductTag { get; set; }
        
        [Display(Name = "Product Brand")]
        public int ProductBrandId { get; set; }
        //[ForeignKey("ProductBrandId")]
        public virtual ProductBrand? ProductBrand { get; set; }

        [NotMapped]
        public string? BrandName { get; set; }

    }
}
