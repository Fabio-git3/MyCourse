using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    //messo qui funziona su tutte le action di questo controller
    //[ResponseCache(CacheProfileName="Home")]
    public class HomeController: Controller
    {
        //[ResponseCache(Duration=60, Location=ResponseCacheLocation.Client)]
        [ResponseCache(CacheProfileName="Home")]
         public IActionResult Index()
        {
            ViewData["Title"]="Benvenuto su MyCourse";
            return View();
            //return Content("hi");
        }
    }
}