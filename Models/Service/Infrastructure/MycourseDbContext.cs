using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MyCourse.Models.Entities;

namespace MyCourse.Models.Services.Infrastucture
{
    public partial class MycourseDbContext : DbContext
    {
        // public MycourseDbContext()
        // {
        // }

        public MycourseDbContext(DbContextOptions<MycourseDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Lesson> Lessons { get; set; }

//         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//         {
//             if (!optionsBuilder.IsConfigured)
//             {
// #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                 optionsBuilder.UseSqlite("Data Source=Data/MyCourse.db");
//             }
//         }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
           
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Courses");//superfluo se il nome della tabella è lo stesso della proprieta che espone il dbset
                entity.HasKey(course => course.Id);  //superfluo se il campo si chiama id o courseid
                //entity.HasKey(course =>new {course.Id,course.Author});// esempio se la key è composta da piu campi

                // Mapping owns type
                //entity.OwnsOne(Course=>Course.CurrentPrice); // se le colonne hanno lo stesso nome delle proprieta ex CurrentPrice_Currency
                entity.OwnsOne(Course=>Course.CurrentPrice, builder=>{
                    builder.Property(money=>money.Currency)
                    .HasConversion<string>()
                    .HasColumnName("CurrentPrice_Currency");
                    builder.Property(money=>money.Amount)
                    .HasColumnName("CurrentPrice_Amount");
                });// questo è superfluo perche le nostre colonne gia seguono la convenzione dei nomi

                entity.OwnsOne(Course=>Course.FullPrice,builder=>{
                    builder.Property(money=>money.Currency)
                    .HasConversion<string>();
                });


                //Mapping per le relazioni
                entity.HasMany(course=>course.Lessons)
                .WithOne(lesson=>lesson.Course)
                .HasForeignKey(lesson=>lesson.CourseId);////superfluo se il campo si chiama  courseid



                #region creato da reverse engineering
                
                 /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Author)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.Property(e => e.CurrentPriceAmount)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.CurrentPriceCurrency)
                    .IsRequired()
                    .HasColumnName("CurrentPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Email).HasColumnType("TEXT (100)");

                entity.Property(e => e.FullPriceAmount)
                    .IsRequired()
                    .HasColumnName("FullPrice_Amount")
                    .HasColumnType("NUMERIC")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.FullPriceCurrency)
                    .IsRequired()
                    .HasColumnName("FullPrice_Currency")
                    .HasColumnType("TEXT (3)")
                    .HasDefaultValueSql("'EUR'");

                entity.Property(e => e.ImagePath).HasColumnType("TEXT (100)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");
                      */
            #endregion

            });

            modelBuilder.Entity<Lesson>(entity =>
            {
                entity.HasOne(lesson=>lesson.Course)//alternativo a farlo dall'entita corsi
                .WithMany(course=>course.Lessons);


                #region 
                /*
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasColumnType("TEXT (10000)");

                entity.Property(e => e.Duration)
                    .IsRequired()
                    .HasColumnType("TEXT (8)")
                    .HasDefaultValueSql("'00:00:00'");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnType("TEXT (100)");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.CourseId);
                    */
                #endregion


            });
          
        }
    }
}
