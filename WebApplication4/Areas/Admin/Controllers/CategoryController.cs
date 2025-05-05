using Microsoft.AspNetCore.Mvc;
using WebApplication4.DAL;
using WebApplication4.Models;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {

                return View(category);
            }


            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");                                                             

        }
        public IActionResult Edit(int id)
        {
            var category=_context.Categories.FirstOrDefault(x=>x.Id==id);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category,int id) 
        {
            var categories = _context.Categories.FirstOrDefault(x => x.Id == id);

            categories.Name = category.Name;
            
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var category=_context.Categories.FirstOrDefault(x=>x.Id==id);
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
