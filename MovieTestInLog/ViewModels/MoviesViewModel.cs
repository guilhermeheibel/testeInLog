using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using MovieTestInLog.Models;
using MovieTestInLog.Views;
using MovieTestInLog.Abstractions;
using System.Windows.Input;
using MovieTestInLog.UI.Utils;
using Xamarin.Forms.Extended;
using System.Linq;

namespace MovieTestInLog.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<MoviesModel> ItemsMovie { get; }
        private int CountPages = 1;
        public ICommand ShowMovieDetailCommand { get; }
        public MoviesViewModel()
        {
            Title = "Movies List";
            ItemsMovie = new InfiniteScrollCollection<MoviesModel>();
            ShowMovieDetailCommand = new Command<MoviesModel>(async (x) => await ExecuteMovieDetail(x));
            ItemsMovie = new InfiniteScrollCollection<MoviesModel>
            {
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    if (!string.IsNullOrEmpty(SearchText)) return null;
                    CountPages++;
                    var moviesList = await HubService.GetMoviesAsync(CountPages);
                    foreach (var itemMovie in moviesList)
                    {
                        itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                        ItemsMovie.Add(itemMovie);
                    }
                    IsBusy = false;
                    return moviesList;
                }
            };
        }

        private async Task ExecuteMovieDetail(MoviesModel movieSelected)
        {
            await PushAsync<MoviesDetailViewModel>(movieSelected);
        }

        public override async Task LoadAsync()
        {

            IsBusy = true;
            ItemsMovie.Clear();
            var moviesList = await HubService.GetMoviesAsync(1);
            foreach (var itemMovie in moviesList)
            {
                itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                ItemsMovie.Add(itemMovie);
            }
            OnPropertyChanged(nameof(ItemsMovie));
            IsBusy = false;
        }

        private string _searchText = string.Empty;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetProperty(ref _searchText, value);
                if (!string.IsNullOrEmpty(value))
                    ExecuteSearchCommand();

            }
        }
        private async void ExecuteSearchCommand()
        {
            CountPages = 1;
            ItemsMovie.Clear();
            var itemsInfiniteSearch = new InfiniteScrollCollection<MoviesModel>
            {
                OnLoadMore = async () =>
                {

                    IsBusy = true;
                    CountPages++;
                    if (CountPages == 1) ItemsMovie.Clear();
                    var moviesList = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());
                    foreach (var itemMovie in moviesList.results)
                    {
                        itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                        ItemsMovie.Add(itemMovie);
                    }
                    IsBusy = false;
                    return moviesList.results;
                }
            };

            var movies = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());

            if (CountPages == 1)
                ItemsMovie.Clear();
            foreach (var item in movies.results)
            {
                item.poster_path = PathMoviesImage.PathConverter(item.id.ToString(), item.poster_path);

                ItemsMovie.Add(item);
            }
            OnPropertyChanged(nameof(ItemsMovie));
        }

    }
}