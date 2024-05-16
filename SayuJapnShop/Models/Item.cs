using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Item
    {
        public int ItemId { get; set; }

        [Required(ErrorMessage = "Select Category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string ItemName { get; set; } = null!;

        [Required(ErrorMessage = "Choose Status")]
        public string status { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;

        public virtual Purchase Purchases { get; } = null!;

        public virtual Sale Sales { get; } = null!;

        public virtual ICollection<Export> Exports { get; } = new List<Export>();
        public virtual ICollection<Import> Imports { get; } = new List<Import>();
    }
}
