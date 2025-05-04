using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApplication4.DAL;
using WebApplication4.Helper;
using WebApplication4.Models;

namespace WebApplication4.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var slides = _context.Slides.ToList();
            return View(slides);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slide slide, IFormFile file)
        {
            string filename = Guid.NewGuid() + file.FileName;
            string path = Path.Combine(_env.WebRootPath, "upload", filename);
            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            slide.PhotoUrl = "/upload/" + filename;
            await _context.Slides.AddAsync(slide);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var slide = _context.Slides.FirstOrDefault(x => x.Id == id);
            if (slide == null) return NotFound();

            return View(slide);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(int id, IFormFile file, Slide updateSlide)
        {
            var slide = _context.Slides.FirstOrDefault(x => x.Id == id);
            if (slide == null) return NotFound();

            if (file != null)
            {
                string webrootpath = _env.WebRootPath;
                string folder = "upload";

               
                string fileurl = await file.SaveFileAsync(webrootpath, folder);

                
                slide.PhotoUrl = fileurl;
            }

            slide.Discount = updateSlide.Discount;
            slide.Title = updateSlide.Title;
            slide.SubTitle = updateSlide.SubTitle;

            _context.Slides.Update(slide);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {
            var slide = _context.Slides.FirstOrDefault(x => x.Id == id);
            if (slide == null) return NotFound();
            _context.Slides.Remove(slide);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
