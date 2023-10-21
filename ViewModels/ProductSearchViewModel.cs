using TechStore.Models;

namespace TechStore.ViewModels
{
    public class ProductSearchViewModel
    {
        public IEnumerable<Product>? products { get; set; }
        public IEnumerable<ProductBrand>? productBrands { get; set; }
        public int productBrandId { get; set; } = 0;
        public string SearchWord { get; set; } = "";
    }
}
