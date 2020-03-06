using MovieTestInLog.Models;
using MovieTestInLog.UI.Utils;
using System.Threading.Tasks;

namespace MovieTestInLog.ViewModels
{
    public class MoviesDetailViewModel : BaseViewModel
    {
        private MoviesModel SelectedMovieDetail { get; }
        public MoviesDetailModel MovieDetail { get; set; }
        public MoviesDetailViewModel(MoviesModel selectedMovie)
        {
            SelectedMovieDetail = selectedMovie;
        }
        public override async Task LoadAsync()
        {
            IsBusy = true;
            MovieDetail = await HubService.GetMoviesDetailAsync(SelectedMovieDetail.id);

            MovieDetail.poster_path  = PathMoviesImage.PathConverter(SelectedMovieDetail.id.ToString(), MovieDetail.poster_path);

            OnPropertyChanged(nameof(MovieDetail));
            IsBusy = false;
        }
    }
}
