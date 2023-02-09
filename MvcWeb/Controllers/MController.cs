using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MongoDB.Bson;
using MongoDB.Driver;
using MvcWeb.Models;
using MvcWeb.SchoolModels.SchoolViewModels;

namespace MvcWeb.Controllers
{
    public class MController : Controller
    {
        // GET: MController
        private readonly ILogger<MController> _logger;
        protected MongoClient Client { get; set; }
        //protected MongoDatabase Database { get; set; } 
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


        public async Task<ActionResult> GenUsers()
        {
            // need to port-fwd 100 --- kubernetes 
                Client = new MongoClient("mongodb://root:rootpassword@192.168.1.100:27017");

                var databasehig = Client.GetDatabase("hibigglus");
                Client.DropDatabase("hibigglus");
                Client.DropDatabase("hubigglus");

                var database = Client.GetDatabase("SampleDB");
                var databases =  await  Client.ListDatabasesAsync();
                while (databases.MoveNext())
                    {
                        var currentBatch = databases.Current;
                        //Utils.Log(currentBatch.AsEnumerable(), "List databases");
                        foreach (BsonDocument s in currentBatch)
                        {
                     
                            _logger.Log(LogLevel.Information, "abc");
                        }
                    }

                var travelers = RandomData.GenerateTravelers(10, 5);
                var persons = RandomData.GenerateUsers(30);
            //  dynamic model = new System.Dynamic.ExpandoObject();
            //  model.Customers = travelers;
            //  model.Employees = persons;
            MultiList model = new MultiList();
            model.travelers = travelers;
            model.user = persons;

            /*
            MultiList ml = new MultiList();
            ml.travelers = travelers;
            ml.user = persons;
            //ViewData["travelers"] = travelers;
            // ViewData["persons"] = persons;
            */

            //List<object> results = new List<object>();
            //results.Add(travelers);
            //results.Add(persons);

            return View(model);
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
        public ActionResult Delete(string id)
        {
            return View();
        }

        // POST: MController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
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
