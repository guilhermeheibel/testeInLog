using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTestInLog.UI.Utils
{
    static class PathMoviesImage
    {
        static public string PathConverter(string movie_id, string pathImg)
        {
            return "http://image.tmdb.org/t/p/w185/" + pathImg;
        }

    }
}
