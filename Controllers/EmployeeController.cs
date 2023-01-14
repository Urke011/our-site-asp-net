using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using our_site_asp_net.Models;
namespace our_site_asp_net.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PeopleContext peopleContext;

        public EmployeeController(PeopleContext peopleContext)//ctrl . za stvaranje polja 
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
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task <IActionResult> View(Guid id)
        {
           var employee = await peopleContext.people.FirstOrDefaultAsync(x=>x.id == id);
            if(employee != null)
            {
                var viewModel = new UpdateViewModel()
                {
                    id = employee.id,
                    employeName = employee.employeName,
                    employeImage = employee.employeImage,
                    position = employee.position,
                    schwerpunkte = employee.schwerpunkte,
                    askMyanyThing = employee.askMyanyThing
                };
                return await Task.Run(()=>View("View",viewModel));
            }
           
         return  RedirectToAction("Index");
        }
        //update
        public async Task<IActionResult> View(UpdateViewModel model)
        {
            var employee = await peopleContext.people.FindAsync(model.id);
            if(employee != null)
            {
                employee.employeName = model.employeName;
                employee.employeImage = model.employeImage;
                employee.position = model.position;
                employee.schwerpunkte = model.schwerpunkte;
                employee.askMyanyThing = model.askMyanyThing;
              await  peopleContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateViewModel model)
        {
            var employee = await peopleContext.people.FindAsync(model.id);
            if (employee != null)
            {
                peopleContext.people.Remove(employee);
                await peopleContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
