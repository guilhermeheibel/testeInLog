using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MovieTestInLog.Models
{
    public class MoviesDetailModel
    {

        public bool adult { get; set; }
        public string backdrop_path { get; set; }
        public object belongs_to_collection { get; set; }
        public int budget { get; set; }
        public string despesas { get { return "Despesas: " + string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", budget); } }
       
        public Genre[] genres { get; set; }
        public string homepage { get; set; }
        public int id { get; set; }
        public string imdb_id { get; set; }
        public string original_language { get; set; }
        public string original_title { get; set; }
        public string overview { get; set; }
        public float popularity { get; set; }
        public string poster_path { get; set; }
        public Production_Companies[] production_companies { get; set; }
        public Production_Countries[] production_countries { get; set; }
        public DateTimeOffset? release_date { get; set; }
        public string dataLancamento { get { return "Data lançamento: " + release_date?.ToString("dd/MM/yyyy"); } }
        public int revenue { get; set; }
        public string receita { get { return "Receita: " + string.Format(CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:#,###.##}", revenue); } }

        string tempo { get { return "Tempo de execução: " + runtime; } }
        public int runtime { get; set; }
        public Spoken_Languages[] spoken_languages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool video { get; set; }

        public string media { get { return "Média de votos: " + vote_average; } }
        public float vote_average { get; set; }
        public int vote_count { get; set; }


        public class Genre
        {
            public int id { get; set; }
            public string name { get; set; }
        }

        public class Production_Companies
        {
            public int id { get; set; }
            public string logo_path { get; set; }
            public string name { get; set; }
            public string origin_country { get; set; }
        }

        public class Production_Countries
        {
            public string iso_3166_1 { get; set; }
            public string name { get; set; }
        }

        public class Spoken_Languages
        {
            public string iso_639_1 { get; set; }
            public string name { get; set; }
        }

    }
}
