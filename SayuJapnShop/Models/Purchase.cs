using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Purchase
    {
        [Required(ErrorMessage = "Select Item")]
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Enter Cost")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Enter Price")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Enter Qty")]
        public int Qty { get; set; }

        [Required(ErrorMessage = "Choose Date")]
        public string Date { get; set; } = null!;

        public virtual Item Item { get; set; } = null!;
    }
}
