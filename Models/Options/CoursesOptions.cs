

namespace MyCourse.Models.Options
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class CoursesOptions
    {
        public long PerPage { get; set; }

        public CoursesOrderOption Order { get; set; }
    }

    public partial class CoursesOrderOption
    {
        public string By { get; set; }
     
        public bool Ascending { get; set; }
   
        public string[] Allow { get; set; }
    }

   
}

