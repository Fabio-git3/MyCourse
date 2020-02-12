using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCourse.Models.InputModels;
using MyCourse.Models.Services.Application;
using MyCourse.Models.ViewModels;

namespace MyCourse.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService courseService;
        public CoursesController(ICachedCourseService courseService)
        {
            this.courseService = courseService;

        }
        public async Task<IActionResult> Index(CourseListInputModel model)
        {
            ViewData["Title"] = "Catalogo dei corsi!!";
            //var courseService= new CourseService();
            List<CourseViewModel> courses =await courseService.GetCoursesAsync(model);
            return View(courses);
        }

        public async Task<IActionResult> Detail(int id)
        {
            //var courseService= new CourseService();
            CourseDetailViewModel viewModel =await courseService.GetCourseAsync(id);
            ViewData["Title"] = viewModel.Title;
            return View(viewModel);
        }


    }
}