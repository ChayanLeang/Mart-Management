using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class LogIn
    {
        [Required(ErrorMessage = "Enter UserName")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Enter Password")]
        public string Password { get; set; } = null!;
    }
}
