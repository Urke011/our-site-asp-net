using Microsoft.AspNetCore.Mvc;
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
