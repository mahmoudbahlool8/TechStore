using Microsoft.Build.Framework;

namespace TechStore.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Order order { get; set; }
        public virtual Product product { get; set; }
    }
}
