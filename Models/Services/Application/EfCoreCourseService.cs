using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyCourse.Models.Entities;
using MyCourse.Models.InputModels;
using MyCourse.Models.Options;
using MyCourse.Models.Services.Infrastucture;
using MyCourse.Models.ViewModels;

namespace MyCourse.Models.Services.Application
{
    public class EfCoreCourseService : ICourseService
    {
        private readonly MycourseDbContext dbContext;
        private readonly IOptionsMonitor<CoursesOptions> coursesOptions;

        public EfCoreCourseService(MycourseDbContext dbContext,IOptionsMonitor<CoursesOptions> coursesOptions)
        {
            this.dbContext = dbContext;
            this.coursesOptions = coursesOptions;
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

        public async Task<List<CourseViewModel>> GetCoursesAsync(CourseListInputModel model)
        {
            //  page= Math.Max(1,);
            // int limit=(int)(coursesOptions.CurrentValue.PerPage);
            // int offset=(page-1) * limit;
            // search=search ?? "";

            //  var orderOptions= coursesOptions.CurrentValue.Order;
            // if(!orderOptions.Allow.Contains(orderby))
            // {
            //     orderby=orderOptions.By;
            //     ascending=orderOptions.Ascending;
            // }

            IQueryable<Course> baseQuery = dbContext.Courses;

           switch(model.OrderBy)
            {
                case "Title":
                if(model.Ascending) {
                baseQuery= baseQuery.OrderBy(course => course.Title);
                }else{
                    baseQuery = baseQuery.OrderByDescending(course => course.Title);
                }
                break;

                case "Rating":
                if(model.Ascending) {
                baseQuery= baseQuery.OrderBy(course => course.Rating);
                }else{
                    baseQuery = baseQuery.OrderByDescending(course => course.Rating);
                }
                break;

                case "CurrentPrice":
                if(model.Ascending) {
                baseQuery= baseQuery.OrderBy(course => course.CurrentPrice.Amount);
                }else{
                    baseQuery = baseQuery.OrderByDescending(course => course.CurrentPrice.Amount);
                }
                break;
               
            };

            IQueryable<CourseViewModel>queryLinq= dbContext.Courses
            .Where(course => course.Title.Contains(model.Search))
            .Skip(model.Offset)
            .Take(model.Limit)
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