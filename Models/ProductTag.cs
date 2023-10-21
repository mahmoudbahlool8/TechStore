using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class ProductTag
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Display(Name ="Tag Name")]
        public string TagName { get; set; }
    }
}
