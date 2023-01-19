using System.ComponentModel.DataAnnotations;


namespace our_site_asp_net.Models
{
    public class User
    {
        //treba biti string
        public string Id { get; set; }

        [Required, MinLength(2,ErrorMessage="Minimum length is 2")]
        public string UserName { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password), Required, MinLength(4, ErrorMessage = "Minimum length is 4")]
        public string  Password { get; set; }

    }
}
