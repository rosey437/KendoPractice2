using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KendoPractice2.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace KendoPractice2.Controllers
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
            return View();
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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(PersonModel newPerson)
        {
            var serviceDal = new PeopleService();

            serviceDal.SavePerson(newPerson);

            return View();
        }


        [HttpGet]
        public IActionResult Report()
        {
          
            return View();
        }

        public JsonResult PersonList([DataSourceRequest]      DataSourceRequest request)
        {
            try
            {
                PeopleService serviceDal = new PeopleService();
                List<PersonModel> people = serviceDal.GetPeople();
                DataSourceResult result = people.ToDataSourceResult(request);

                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

    }
}
