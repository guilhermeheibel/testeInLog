using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTestInLog.Models
{
    public class MoviesModel
    {
        public float popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public string poster_path { get; set; }
        public int id { get; set; }
        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public int[] genre_ids { get; set; }
        public string title { get; set; }
        public float vote_average { get; set; }
        public string Pontuacao { get { return "Nota: " + vote_average; } }
        public string DataLancamento { get { return "Lançamento: " + release_date.ToString("dd/MM/yyyy"); } }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
    }
}
