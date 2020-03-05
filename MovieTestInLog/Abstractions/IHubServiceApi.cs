using MovieTestInLog.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestInLog.Abstractions
{
    public interface IHubServiceApi
    {
        Task<IEnumerable<MoviesModel>> GetMoviesAsync(int SelectedPage);
        Task<MoviesDetailModel> GetMoviesDetailAsync(int movie_id);
        Task<IEnumerable<MoviesModel>> GetSearchMovieAsync(string searchText, string page);
    } 
}
