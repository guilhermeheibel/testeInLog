using MovieTestInLog.Models;
using MovieTestInLog.UI.Utils;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static MovieTestInLog.Models.MoviesDetailModel;

namespace MovieTestInLog.ViewModels
{
    public class MoviesDetailViewModel : BaseViewModel
    {
        private MoviesModel SelectedMovieDetail { get; }
        public MoviesDetailModel MovieDetail { get; set; }

        public ObservableCollection<Genre> Genre { get; }
        public MoviesDetailViewModel(MoviesModel selectedMovie)
        {
            SelectedMovieDetail = selectedMovie;
            Genre = new ObservableCollection<Genre>();
        }
        public override async Task LoadAsync()
        {
            IsBusy = true;
            MovieDetail = await HubService.GetMoviesDetailAsync(SelectedMovieDetail.id);
            if (MovieDetail == null)
            {
                await Navigation.PopToRootAsync();
                return;
            }
            MovieDetail.backdrop_path = PathMoviesImage.PathConverter(SelectedMovieDetail.id.ToString(), MovieDetail.backdrop_path);
            if (MovieDetail != null && MovieDetail.genres != null)
            {
                Genre.Clear();
                foreach (var item in MovieDetail.genres)
                {
                    Genre.Add(item);
                }
            }
            OnPropertyChanged(nameof(MovieDetail));
            OnPropertyChanged(nameof(Genre));
            IsBusy = false;
        }
    }
}
