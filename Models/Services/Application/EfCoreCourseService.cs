using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyCourse.Models.Services.Infrastucture;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MycourseDbContext dbContext;

        public EfCoreCourseService(MycourseDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {
            CourseDetailViewModel detailViewModel=await dbContext.Courses
            .AsNoTracking()
            .Where(course=>course.Id==id)
            .Select(course=>new CourseDetailViewModel{
                Id=course.Id,
                Title=course.Title,
                Description=course.Description,
                ImagePath=course.ImagePath,
                Author=course.Author,
                Rating=course.Rating,
                CurrentPrice=course.CurrentPrice,
                FullPrice=course.FullPrice,
                Lessons=course.Lessons.Select(lesson=>new LessonViewModel{
                Id = lesson.Id,
                Title = lesson.Title,
                Description = lesson.Description,
                Duration =lesson.Duration 
                }).ToList()

            }).SingleAsync();//restituisce il primo elemento di un elenco se nn trova nulla restituisce eccezione
            return detailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            IQueryable<CourseViewModel>queryLinq= dbContext.Courses
            .AsNoTracking()
            .Select(course=>
            new CourseViewModel{
                Id=course.Id,
                Title=course.Title,
                ImagePath=course.ImagePath,
                Author=course.Author,
                Rating=course.Rating,
                CurrentPrice=course.CurrentPrice,
                FullPrice=course.FullPrice
            });//.ToListAsync();//è qui che viene inviata la query al db perche è qui che rendiamo effettiva la nostra volonta d leggere dai dal db
            
            List<CourseViewModel> courses=await queryLinq.ToListAsync();
            return courses;
        }
    }
}