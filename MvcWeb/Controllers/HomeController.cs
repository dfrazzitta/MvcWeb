using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MvcWeb.Models;
using System.Diagnostics;

namespace MvcWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected MongoClient Client { get; set; }
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.Log(LogLevel.Trace, "home/Index");
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
             
            Client = new MongoClient("mongodb://10.244.3.198:27017,mongodb-replica-1:27017/?replicaSet=rs0");
            var rs = Client.Settings.ReplicaSetName;
            ViewData["client"] = rs;
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