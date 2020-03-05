using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTestInLog.Models
{
    public class MovieSearchModel
    {
            public int page { get; set; }
            public int total_results { get; set; }
            public int total_pages { get; set; }
            public MoviesModel[] results { get; set; }
      
    }
}
