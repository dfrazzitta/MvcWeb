using Microsoft.AspNetCore.Mvc;
using MvcWeb.Models;
using System.Diagnostics;

namespace MvcWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Log(LogLevel.Trace, "home/Index");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.Log(LogLevel.Information, "home/Privacy");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}