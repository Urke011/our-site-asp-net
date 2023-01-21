using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using our_site_asp_net.Models.ViewModels;

namespace our_site_asp_net.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> signInManeger;

        public AccountController(SignInManager<IdentityUser> signInManeger)
        {
            this.signInManeger = signInManeger;
        }
        public IActionResult Login(string ReturnUrl) => View(ReturnUrl);

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            //its need to be ModelState.IsValid
            if (loginVm != null)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await signInManeger.PasswordSignInAsync(loginVm.UserName,loginVm.Password,false,false);

                if (result.Succeeded)
                {
                    return Redirect(loginVm.ReturnUrl??"/");
                }

                    ModelState.AddModelError("", "Invalid username or password");
           
            }
            return View(loginVm);
        }
        public IActionResult Details() => View(new AuthDetailsViewModel
        {
            Cookie = Request.Cookies[".AspNetCore.Identity.Application"]

        });
        //logout
        public async Task<RedirectResult> Logout(string returnUrl = "/")
        {
            await signInManeger.SignOutAsync();

            return Redirect(returnUrl);
        }
    }
}
