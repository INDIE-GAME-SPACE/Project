using IGS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IGS.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            Cube cube = new Cube
            {
                x = 1,
                y = 1,
                z = 1
            };

            ViewBag.Message = cube;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}