using System;
using System.Collections.Generic;
using MyCourse.Models.ValueTypes;

namespace MyCourse.Models.Entities
{
    public partial class Course
    {
        public Course(string title, string author)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("il campo Titolo deve essere valorizzato");
            }

            if(string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException("il campo Autore deve essere valorizzato");
            }

            Title=title;
            Author=author;
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; private set; }
        public string Title { get;private set; }
        public string Description { get;private set; }
        public string ImagePath { get;private set; }
        public string Author { get;private set; }
        public string Email { get;private set; }
        public double Rating { get;private set; }
        public Money FullPrice { get;private set; }
        public Money CurrentPrice { get;private set; }

        public void ChangePrices(Money newFullPrice,Money newDiscountPrice){

             if(newFullPrice==null|| newDiscountPrice==null)
            {
                throw new ArgumentException("i campi dei prezzi devono essere valorizzati");
            }

            if(newFullPrice.Currency!= newDiscountPrice.Currency)
            {
                throw new ArgumentException("i campi dei prezzi devono essere della stessa valuta");
            }

            if(newFullPrice.Amount < newDiscountPrice.Amount)
            {
                throw new ArgumentException("il prezzo scontato deve essere inferiore a quello scontato");
            }

            FullPrice=newFullPrice;
            CurrentPrice=newDiscountPrice;

        }
        public void ChangeTitle(string newTitle){
           
           
            if(string.IsNullOrWhiteSpace(newTitle))
            {
                throw new ArgumentException("il campo Titolo deve essere valorizzato");
            }
            Title=newTitle;
        }

        public virtual ICollection<Lesson> Lessons { get;private set; }
    }
}
