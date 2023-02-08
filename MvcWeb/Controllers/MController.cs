using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MongoDB.Driver;
using MvcWeb.Models;

namespace MvcWeb.Controllers
{
    public class MController : Controller
    {
        // GET: MController
        private readonly ILogger<MController> _logger;
        protected MongoClient Client { get; set; }
        protected IMongoDatabase Database { get; set; } 
        public MController(ILogger<MController> logger)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
           // Client = new MongoClient("mongodb://root:rootpassword@192.168.1.100:27017"); 

           // Database = Client.GetDatabase("SampleDB");

            
            var travelers = RandomData.GenerateTravelers(10, 5);
            var persons = RandomData.GenerateUsers(30);


            return View(travelers);
        }


        public ActionResult GenUsers()
        {
            // Client = new MongoClient("mongodb://root:rootpassword@192.168.1.100:27017"); 

            // Database = Client.GetDatabase("SampleDB");


           // var travelers = RandomData.GenerateTravelers(10, 5);
            var persons = RandomData.GenerateUsers(30);


            return View(persons);
        }
        // GET: MController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MController/Create
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

        // GET: MController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MController/Edit/5
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

        // GET: MController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MController/Delete/5
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
