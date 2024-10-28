using EcommerceWithDefaultIdentity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using EcommerceWithDefaultIdentity.Data;

namespace EcommerceWithDefaultIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductDbContext _context;

        public HomeController(ILogger<HomeController> logger, ProductDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
