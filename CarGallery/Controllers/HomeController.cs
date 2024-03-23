using CarGallery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CarGallery.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarGalleryContext _context;
        public HomeController(ILogger<HomeController> logger, CarGalleryContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var carros = _context.Carros.ToList();

            ViewBag.Carros = carros;

            return View();
        }

        public IActionResult Search([FromQuery] int? id)
        {
            List<Carro> carros;

            if (id == null)
            {
                ViewBag.Carros = _context.Carros.ToList(); 
                return View();
            }

            ViewBag.Carros = _context.Fabricantes
                                     .Include(x => x.Carros)   
                                     .FirstOrDefault(f => f.Id == id).Carros;

            return View();

        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
