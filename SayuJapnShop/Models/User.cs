using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Enter Email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Enter Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Choose Role")]
        public string Role { get; set; } = null!;
    }
}

