using System.Collections.Generic;
using System.Data;
using MyCourse.Models.Services.Infrastucture;
using MyCourse.Models.ViewModels;
using MyCourse.Models.Exceptions;
using System;
using System.Threading.Tasks;

namespace MyCourse.Models.Services.Application
{
    public class AdoNetCourseService : ICourseService
    {
        private readonly IDataBaseAccessor db;
        public AdoNetCourseService(IDataBaseAccessor db)
        {
            this.db = db;

        }
        public async Task<CourseDetailViewModel> GetCourseAsync(int id)
        {

            FormattableString query = $@"SELECT Id, Title, Description, ImagePath, Author, Rating, FullPrice_Amount, FullPrice_Currency, CurrentPrice_Amount, CurrentPrice_Currency FROM Courses WHERE Id={id}
            ; SELECT Id, Title, Description, Duration FROM Lessons WHERE CourseId={id}";

            DataSet dataSet =await  db.QueryAsync(query);

            //Course
            var courseTable = dataSet.Tables[0];
            if (courseTable.Rows.Count != 1)
            {
                //logger.LogWarning("Course {id} not found", id);
                throw new CourseNotFoundException(id);
            }
            var courseRow = courseTable.Rows[0];
            var courseDetailViewModel = CourseDetailViewModel.FromDataRow(courseRow);

            //Course lessons
            var lessonDataTable = dataSet.Tables[1];

            foreach (DataRow lessonRow in lessonDataTable.Rows)
            {
                LessonViewModel lessonViewModel = LessonViewModel.FromDataRow(lessonRow);
                courseDetailViewModel.Lessons.Add(lessonViewModel);
            }
            return courseDetailViewModel;
        }

        public async Task<List<CourseViewModel>> GetCoursesAsync()
        {
            FormattableString query= $"SELECT Id,Title,ImagePath,Author,Rating,FullPrice_Amount,FullPrice_Currency,CurrentPrice_Amount,CurrentPrice_Currency FROM COURSES";
            DataSet dataSet=await db.QueryAsync(query);
            var dataTable=dataSet.Tables[0];
            List<CourseViewModel> courseList=new List<CourseViewModel>();
            foreach(DataRow courseRow in dataTable.Rows)
            {
                CourseViewModel course=CourseViewModel.FromDataRow(courseRow);
                courseList.Add(course);
            }

            return courseList;
        }
    }
}