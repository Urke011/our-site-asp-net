using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using our_site_asp_net.Models;

namespace our_site_asp_net.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly PeopleContext peopleContext;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(PeopleContext peopleContext, IWebHostEnvironment webHostEnvironment)//ctrl . za stvaranje polja 
        {
            this.peopleContext = peopleContext;
            this.webHostEnvironment = webHostEnvironment;
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
        public async Task<IActionResult> Add(EmployeProfile addEmployeeRequest)
        {
            //upload Img

            if (addEmployeeRequest.EmployePhoto != null)
            {
                string folder = "workers/images/";
                folder += addEmployeeRequest.EmployePhoto.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                addEmployeeRequest.photoUrl = "/"+folder;
                //save img
                addEmployeeRequest.EmployePhoto.CopyTo(new FileStream(serverFolder,FileMode.Create));
            }

            var Employe = new EmployeProfile()
            {
                id = addEmployeeRequest.id,
                employeName = addEmployeeRequest.employeName,
                //employeImage = addEmployeeRequest.employeImage,
                position = addEmployeeRequest.position, 
                schwerpunkte = addEmployeeRequest.schwerpunkte,
                askMyanyThing = addEmployeeRequest.askMyanyThing,
                photoUrl = addEmployeeRequest.photoUrl
            };
            await peopleContext.people.AddAsync(Employe);
            await peopleContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task <IActionResult> View(int id)
        {
           var employee = await peopleContext.people.FirstOrDefaultAsync(x=>x.id == id);
            if(employee != null)
            {
                var viewModel = new EmployeProfile()
                {
                    id = employee.id,
                    employeName = employee.employeName,
                    photoUrl = employee.photoUrl,
                    position = employee.position,
                    schwerpunkte = employee.schwerpunkte,
                    askMyanyThing = employee.askMyanyThing
                };
                return await Task.Run(()=>View("View",viewModel));
            }
           
         return  RedirectToAction("Index");
        }
        //update
        public async Task<IActionResult> View(EmployeProfile model)
        {

            if (model.EmployePhoto != null)
            {
                string folder = "workers/images/";
                folder += model.EmployePhoto.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                model.photoUrl = "/" + folder;
                //save img
                model.EmployePhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            var employee = await peopleContext.people.FindAsync(model.id);
            if(employee != null)
            {
                employee.employeName = model.employeName;
                employee.photoUrl = model.photoUrl;
                employee.position = model.position;
                employee.schwerpunkte = model.schwerpunkte;
                employee.askMyanyThing = model.askMyanyThing;
              await  peopleContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeProfile model)
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
