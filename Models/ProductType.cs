using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class ProductType
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name ="Type Name")]
        public string TypeName { get; set; }
    }
}
