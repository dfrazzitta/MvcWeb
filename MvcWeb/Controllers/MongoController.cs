using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Net;

namespace MvcWeb.Controllers
{
    public class MongoController : Controller
    {
        private readonly ILogger<MongoController> _logger;
        private IConfiguration _configuration;
        public MongoController(ILogger<MongoController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        // GET: MongoController
        public ActionResult Index()
        {
            //var Client = new MongoClient(_configuration.GetValue<string>("MongoConnection"));
            IPHostEntry hostInfo1 = Dns.GetHostEntry("www.microsoft.com");
            try
            {
               IPHostEntry hostInfo = Dns.GetHostEntry("mongodb-replica-0.mongo.svc.cluster.local");
                ViewData["goodcall"] = hostInfo.AddressList[0];
            }
            catch {
                ViewData["failedcall"] = "failed DNS call";
            }

             

            return View();
        }

        // GET: MongoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MongoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MongoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MongoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MongoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MongoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MongoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
