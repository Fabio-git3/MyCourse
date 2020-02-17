using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    //messo qui funziona su tutte le action di questo controller
    //[ResponseCache(CacheProfileName="Home")]
    public class HomeController: Controller
    {
        //[ResponseCache(Duration=60, Location=ResponseCacheLocation.Client)]
        [ResponseCache(CacheProfileName="Home")]
         public async Task<IActionResult> Index([FromServices] ICachedCourseService courseService)
        {
            ViewData["Titolo"]="Benvenuto su MyCourse";
            List<CourseViewModel> bestRatingCourses= await courseService.GetBestRatingCoursesAsync();
            List<CourseViewModel> mostRecentCourses = await courseService.GetMostRecentCoursesAsync();
            HomeViewModel viewModel = new HomeViewModel
            {
                BestRatingCourses = bestRatingCourses,
                MostRecentCourses = mostRecentCourses
            };

            return View(viewModel);
            //return Content("hi");
        }
    }
}