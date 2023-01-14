using Microsoft.AspNetCore.Mvc;
using our_site_asp_net.Models;

namespace our_site_asp_net.Controllers
{
    public class BlogController : Controller
    {
        private readonly PeopleContext context;

        public BlogController(PeopleContext context)
        {
            this.context = context;
        }
       
        public IActionResult CreateBlog()
        {
            return View("CreateBlog");
        }
        public IActionResult AddBlog(Blog singleBlog)
        {
            var oneBlog = new Blog()
            {
                Title = singleBlog.Title,
                Author = singleBlog.Author,
                Date = singleBlog.Date,
                TextDescription = singleBlog.TextDescription
            };
            context.Blogs.Add(oneBlog);
            context.SaveChanges();
           return  RedirectToAction("Blog");
        }  

        public IActionResult Blog()
        {
            var allBlogs = context.Blogs.ToList();
            return View(allBlogs);
        }

        public IActionResult Edit(int Id)
        {
            var singleBlog = context.Blogs.FirstOrDefault(b => b.Id == Id);
            if(singleBlog != null)
            {
                var blog = new Blog()
                {
                    Id = singleBlog.Id,
                    Title = singleBlog.Title,
                    Author = singleBlog.Author,
                    Date= singleBlog.Date,
                    TextDescription = singleBlog.TextDescription
                };
                return View(blog);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Edit(Blog model)
        {
            var blog = context.Blogs.Find(model.Id);
            if(blog != null)
            {
                blog.Title = model.Title;
                blog.Author = model.Author;
                blog.Date = model.Date;
                blog.TextDescription = model.TextDescription;
                context.SaveChanges();
                return RedirectToAction("Blog");
            }
            return View();
        }
        public IActionResult Delete(Blog model)
        {
            var blog = context.Blogs.Find(model.Id);
            if( blog != null)
            {
                context.Blogs.Remove(blog);
                context.SaveChanges();
                return RedirectToAction("Blog");
            }
            return RedirectToAction("Blog");
        }
    }
}
