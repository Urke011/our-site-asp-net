using System.ComponentModel.DataAnnotations;

namespace our_site_asp_net.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required, MinLength(2, ErrorMessage = "Minimum length is 2")]
        public string UserName { get; set; }
        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string Password { get; set; }
        public string  ReturnUrl { get; set; }
    }
}
