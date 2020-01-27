using System;
using System.Data;
using MyCourse.Models.Enums;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.ViewModels
{
    public class CourseViewModel
    {
     public int Id  {get;set;}
     public string Title {get;set;}
     public string ImagePath {get;set;}
     public string  Author{get;set;}
     public double  Rating{get;set;}
     public Money  FullPrice{get;set;}
     public Money  CurrentPrice{get;set;}

        public static  CourseViewModel FromDataRow(DataRow courseRow)
        {
            var courseViewModel=new CourseViewModel()
            {
                Id=Convert.ToInt32(courseRow["Id"]),
                Title=(string)courseRow["Title"],
                ImagePath=(string)courseRow["ImagePath"],
                Author=(string)courseRow["Author"],
                Rating=(double)courseRow["Rating"],
                FullPrice=new Money(
                Enum.Parse<Currency>((string)courseRow["FullPrice_Currency"]),
                Convert.ToDecimal(courseRow["FullPrice_Amount"])),
                CurrentPrice=new Money(
                Enum.Parse<Currency>((string)courseRow["CurrentPrice_Currency"]),
                Convert.ToDecimal(courseRow["CurrentPrice_Amount"]))
                
            };
            return courseViewModel;
        }
    }
}