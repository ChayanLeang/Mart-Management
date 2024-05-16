using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Sale
    {
        [Required(ErrorMessage = "Select Item")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Enter Qty")]
        public int Qty { get; set; }

        public virtual Item Item { get; set; } = null!;
    }
}
