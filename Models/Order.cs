using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TechStore.Models
{
    public class Order
    {

        public Order()
        {
            OrderDetails = new List<OrderDetails>();
        }

        public int Id { get; set; }
        [Required]
        [MaxLength(450)]
        public string UserID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [DisplayName("Detailed Address")]
        [MaxLength(1000)]
        public string DetailedAddress { get; set; }
        [Required]
        [DisplayName("Phone Number")]
        [MaxLength (25)]
        public string PhoneNumber { get; set; }
        [DisplayName("Order Time")]
        public DateTime? OrderTime { get; set; }
        public int orderDetails { get; set; }

        public virtual List<OrderDetails> OrderDetails { get; set; }

    }
}
