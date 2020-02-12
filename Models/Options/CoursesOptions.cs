

namespace MyCourse.Models.Options
{

    public partial class CoursesOptions
    {
        public int PerPage { get; set; }

        public CoursesOrderOption Order { get; set; }
    }

    public partial class CoursesOrderOption
    {
        public string By { get; set; }
     
        public bool Ascending { get; set; }
   
        public string[] Allow { get; set; }
    }


}

