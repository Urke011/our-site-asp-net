using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using our_site_asp_net.Models;
using our_site_asp_net.Models.ViewModels;

namespace our_site_asp_net.Areas.Admin.Controllers
{

    
    [Area("Admin")]
    public class UsersController : Controller
    {
        private  UserManager<IdentityUser> userManager;

        public UsersController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (user != null)
            {
                IdentityUser  newUser  = new IdentityUser
                { 
                UserName= user.UserName,
                Email= user.Email
                };
                IdentityResult result = await userManager.CreateAsync(newUser,user.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);
                    }
                
               
            }
            return View(user);
        }
        public async Task<IActionResult> Edit(string Id)
        {
          
            IdentityUser user = await userManager.FindByIdAsync(Id);

            UserViewModel userEdit = new(user);

            return View(userEdit);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                IdentityUser identityUser = await userManager.FindByIdAsync(user.Id);
                identityUser.UserName = user.UserName; 
                identityUser.Email = user.Email;

                IdentityResult result = await userManager.UpdateAsync(identityUser);
                if (result.Succeeded && !String.IsNullOrEmpty(user.Password))
                {
                    await userManager.RemovePasswordAsync(identityUser);
                    result = await userManager.AddPasswordAsync(identityUser, user.Password);
                }
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }


            }
            return View(user);
        }
        public async Task<IActionResult> Delete(string Id)
        {

            IdentityUser user = await userManager.FindByIdAsync(Id);

           if(user != null)
            {
                await userManager.DeleteAsync(user);
            }

            return RedirectToAction("Index");

        }
    }
}
