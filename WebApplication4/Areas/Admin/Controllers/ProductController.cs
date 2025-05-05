using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Models;
using WebApplication4.DAL;
using WebApplication4.Models;

namespace Pronia.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index()
    {
        var products = _context.Products.Include(x => x.photo).Include(x => x.Category).ToList();
        ViewBag.Category = _context.Categories.ToList();

        return View(products);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = _context.Categories.ToList();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(Product product, IFormFile file)
    {

        string filename = Guid.NewGuid() + file.FileName;
        string path = Path.Combine(_env.WebRootPath, "upload", filename);
        using FileStream fileStream = new FileStream(path, FileMode.Create);

        await file.CopyToAsync(fileStream);

        Photo photo = new()
        {
            PhotoUrl = "/upload/" + filename
        };


        product.photo = new List<Photo>();
        product.photo.Add(photo);

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");

    }
}