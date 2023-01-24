using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using our_site_asp_net.Models;

namespace our_site_asp_net.Controllers
{
    public class JobsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly PeopleContext peopleContext;

        public JobsController(IWebHostEnvironment webHostEnvironment, PeopleContext peopleContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.peopleContext = peopleContext;
        }
        public IActionResult CreatorPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreatorPage(Jobs jobRequest)
        {
            //upload Img

            if (jobRequest.JobPhoto != null)
            {
                Guid g = Guid.NewGuid();
                string folder = "image/job-headers/";
                folder += g+jobRequest.JobPhoto.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                jobRequest.photoUrl = "/" + folder;
                //save img
                jobRequest.JobPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }
            var job = new Jobs()
            {
                Id = jobRequest.Id,
                Title = jobRequest.Title,
                photoUrl = jobRequest.photoUrl,
                JobContent = jobRequest.JobContent,
                currentCity = jobRequest.currentCity
            };
            await peopleContext.Jobs.AddAsync(job);
            await peopleContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var jobItem = peopleContext.Jobs.ToList();
            return View(jobItem);
        }
        public IActionResult SingleJob(int id)
        {
            // var singleJob = peopleContext.Jobs.Where(x => x.Id == id).FirstOrDefault();
            var singleJob = peopleContext.Jobs.FromSqlRaw($"select * from Jobs where "+
                $"Id = {id}").ToList();
                 
            return View(singleJob);
        }
        [HttpGet]
        //ovo je stranica gde se pravi edit forma sa akutelnim vrednostima
        public IActionResult Edit(int id)
        {
            var singleJob = peopleContext.Jobs.FirstOrDefault(x => x.Id == id);
            if (singleJob != null)
            {
                var viewModel = new Jobs()
                {
                    Id = singleJob.Id,
                    Title = singleJob.Title,
                    JobPhoto = singleJob.JobPhoto,
                    JobContent = singleJob.JobContent,
                    currentCity = singleJob.currentCity
                    
                };
                return View(viewModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task <IActionResult> Edit(Jobs job)
        {
            if (job.JobPhoto != null)
            {
                Guid g = Guid.NewGuid();
                string folder = "image/job-headers/";
                folder += g+job.JobPhoto.FileName;
                string serverFolder = Path.Combine(webHostEnvironment.WebRootPath, folder);
                job.photoUrl = "/" + folder;
                //save img
                job.JobPhoto.CopyTo(new FileStream(serverFolder, FileMode.Create));
            }

            var singleJob = await peopleContext.Jobs.FindAsync(job.Id);
            if (singleJob != null)
            {
                if(job.photoUrl != null) {
                    singleJob.photoUrl = job.photoUrl;
                }
                
                singleJob.Title = job.Title;
                singleJob.JobContent = job.JobContent;
                singleJob.currentCity = job.currentCity;
                await peopleContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(Jobs job)
        {
            var singleJob = peopleContext.Jobs.Find(job.Id);
            if(singleJob != null)
            {
                peopleContext.Jobs.Remove(singleJob);
                peopleContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
