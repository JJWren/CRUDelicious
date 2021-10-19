using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {
        private MyContext _context;

        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            List<Dish> AllDishes = _context.Dishes.OrderByDescending(dish => dish.Name).ToList();

            return View();
        }

        [HttpGet("/new")]
        public IActionResult NewDish()
        {
            return View();
        }

        [HttpPost("/add")]
        public IActionResult AddDish(Dish NewDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(NewDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("NewDish");
        }

        [HttpGet("Privacy")]
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
