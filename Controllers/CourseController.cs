using Microsoft.AspNetCore.Mvc;

namespace MyCourse.Controllers
{
    public class CourseController:Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail(string id)
        {
            return View();
        }


    }
}