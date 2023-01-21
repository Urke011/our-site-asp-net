using Microsoft.AspNetCore.Identity;

namespace our_site_asp_net.Models.ViewModels
{
    public class AuthDetailsViewModel
    {
        public string Cookie { get; set; }
        public IdentityUser User { get; set; }
    }
}
