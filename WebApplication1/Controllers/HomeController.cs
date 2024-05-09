using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

		public HomeController()
		{
		}

		public ActionResult Welcome(string name, int numTimes = 1)
		{
			ViewBag.Message = "Hello " + name;
			ViewBag.NumTimes = numTimes;

			return View();
		}
        public void TestM(string a1,string a2)
        {
            Console.WriteLine(a1 + " " + a2);
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
    }
}