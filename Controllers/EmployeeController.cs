using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using our_site_asp_net.Models;
namespace our_site_asp_net.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PeopleContext peopleContext;

        public EmployeeController(PeopleContext peopleContext)
        {
            this.peopleContext = peopleContext;
        }
        //[HttpPost]//akcija za prikaz svih clanova tima
        public async Task<IActionResult> Index()
        {
          var employees =  await  peopleContext.people.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddTeamEmployeeModelView addEmployeeRequest)
        {
            var Employe = new EmployeProfile()
            {
                id = Guid.NewGuid(),
                employeName = addEmployeeRequest.employeName,
                employeImage = addEmployeeRequest.employeImage,
                position = addEmployeeRequest.position, 
                schwerpunkte = addEmployeeRequest.schwerpunkte,
                askMyanyThing = addEmployeeRequest.askMyanyThing
            };
            await peopleContext.people.AddAsync(Employe);
            await peopleContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }
    }
}
