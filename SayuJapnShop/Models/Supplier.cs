using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Enter Owner")]
        public string Owner { get; set; } = null!;

        [Required(ErrorMessage = "Enter Company")]
        public string CompanyName { get; set; } = null!;

        [Required(ErrorMessage = "Enter Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Choose Status")]
        public string status { get; set; } = null!;
        public virtual ICollection<Import> Imports { get; } = new List<Import>();
    }
}
