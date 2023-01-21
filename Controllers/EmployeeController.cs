using Microsoft.AspNetCore.Authorization;
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
        //Searchbox
        [HttpGet]
        public async Task<IActionResult> Index(string empsearch)
        {
            ViewData["Getemployeedetails"] = empsearch;

            var empquery = from x in peopleContext.people select x;
            if (!String.IsNullOrEmpty(empsearch))
            {
                empquery = empquery.Where(x => x.employeName.Contains(empsearch) || x.position.Contains(empsearch) || x.schwerpunkte.Contains(empsearch));
            }
            return View(await empquery.AsNoTracking().ToListAsync());
        }

        [HttpGet]
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
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
        [Authorize]
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
                if(model.photoUrl != null)
                {
                    employee.photoUrl = model.photoUrl;
                }
                
                employee.position = model.position;
                employee.schwerpunkte = model.schwerpunkte;
                employee.askMyanyThing = model.askMyanyThing;
              await  peopleContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
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
