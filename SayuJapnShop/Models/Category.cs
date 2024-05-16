using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string CategoryName { get; set; } = null!;

        [Required(ErrorMessage = "Enter Descriptions")]
        public string Descriptions { get; set; } = null!;

        [Required(ErrorMessage = "Choose Status")]
        public string status { get; set; } = null!;
        public virtual ICollection<Item> Items { get; } = new List<Item>();
    }
}


