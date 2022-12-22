using Microsoft.AspNetCore.Mvc;
using our_site_asp_net.Models;
using System.Diagnostics;

namespace our_site_asp_net.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //ne zaboravi static jel se objek brise zbog nepostojanja baze
        private static  List<EmployeProfile> profile = new List<EmployeProfile>();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Datenschutz()
        {
            return View();
        }
        public IActionResult Blog()
        {
            return View();
        }
        public IActionResult Jobs()
        {
            return View();
        }
        public IActionResult Team()
        {
            return View(profile);
        }
        public IActionResult CreateNewProfile()
        {
             
            EmployeProfile profile = new EmployeProfile();
            return View(profile);
        }
        public IActionResult CreateNewProfileForm(EmployeProfile profileNewModel)
        {
            profile.Add(profileNewModel);
            // return View("Team");
            return RedirectToAction(nameof(Team));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}