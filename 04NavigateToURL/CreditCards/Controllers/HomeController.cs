using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CreditCards.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(NoStore = true)]
        public IActionResult Index()
        {
            string[] randomGreetings = { "Hi", "Hey", "Yo" };

            int rndGreetingIndex = new Random().Next(0, randomGreetings.Length);

            ViewData["RandomGreeting"] = randomGreetings[rndGreetingIndex];

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Demo application for Pluralsight course.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
