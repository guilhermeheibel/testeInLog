using System.Threading.Tasks;
using Xamarin.Forms;
using MovieTestInLog.Models;
using System.Windows.Input;
using MovieTestInLog.UI.Utils;
using Xamarin.Forms.Extended;
using System.Linq;
using System.Diagnostics;
using System;
using System.Collections.ObjectModel;

namespace MovieTestInLog.ViewModels
{

    public class CarrouselMoviesViewModel : BaseViewModel
    {
        public ObservableCollection<MoviesModel> ItemsMovie { get; }
        private MoviesModel _selectedMovie;
        public MoviesModel SelectedMovie
        {
            get { return _selectedMovie; }
            set { SetProperty(ref _selectedMovie, value); }
        }
        public ICommand ShowMovieDetailCommand { get; }
        public ICommand ItemTresholdReachedCommand { get; }

        public Command RefreshItemsCommand { get; }
        private int _countPages;
        private bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value); }
        }
        bool SearchInit = false;
        public int CountPages
        {
            get { return _countPages; }
            set { SetProperty(ref _countPages, value); }
        }

        public CarrouselMoviesViewModel()
        {
            Title = "Movies List";
            ;
            ItemsMovie = new InfiniteScrollCollection<MoviesModel>();
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            ShowMovieDetailCommand = new Command(async () => await ExecuteMovieDetail());
            RefreshItemsCommand = new Command(async () =>
            {
                await ExecuteLoadItemsCommand();
                IsRefreshing = false;
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;
            if (!await StatusConnections.VerifyConnect())
            {

                ItemsMovie.Add(new MoviesModel() { title = "Sem conexão ativa com a internet..." });
                IsBusy = false;
                return;
            }
            IsBusy = true;

            try
            {
                CountPages = 1;
                ItemsMovie.Clear();
                var moviesList = await HubService.GetMoviesAsync(CountPages);

                foreach (var item in moviesList)
                {
                    ItemsMovie.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task ExecuteMovieDetail()
        {
            await PushAsync<MoviesDetailViewModel>(SelectedMovie);
        }


        public override async Task LoadAsync()
        {

            SearchInit = true;
            SearchText = null;
            IsBusy = true;
            ItemsMovie.Clear();

            if (!await StatusConnections.VerifyConnect())
            {

                ItemsMovie.Add(new MoviesModel() { title = "Sem conexão..." }); IsBusy = false;
                return;
            }
            CountPages = 1;

            var moviesList = await HubService.GetMoviesAsync(CountPages);
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
                if (SearchInit && value != null)
                    ExecuteSearchCommand();

            }
        }
        private async void ExecuteSearchCommand()
        {

            if (!await StatusConnections.VerifyConnect())
            {

                ItemsMovie.Add(new MoviesModel() { title = "Sem conexão ativa com a internet..." }); IsBusy = false;
                return;
            }
            var itemsInfiniteSearch = new InfiniteScrollCollection<MoviesModel>
            {
                OnLoadMore = async () =>
                {

                    IsBusy = true;
                    CountPages++;
                    if (CountPages == 1) ItemsMovie.Clear();
                    var moviesList = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());
                    if (moviesList == null) return new InfiniteScrollCollection<MoviesModel>();

                    foreach (var itemMovie in moviesList.results)
                    {
                        itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                        ItemsMovie.Add(itemMovie);
                    }
                    IsBusy = false;
                    return moviesList.results;
                }
            };
            if (string.IsNullOrEmpty(SearchText))
            {
                await LoadAsync();
                return;
            }
            IsBusy = true;
            CountPages = 1;
            MovieSearchModel movies = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());
            IsBusy = false;
            if (movies == null) return;



            if (CountPages == 1)
                ItemsMovie.Clear();
            foreach (var item in movies.results)
            {
                item.poster_path = PathMoviesImage.PathConverter(item.id.ToString(), item.poster_path);

                ItemsMovie.Add(item);
            }
            OnPropertyChanged(nameof(ItemsMovie)); IsBusy = false;
        }

        public async Task ItemsTresholdReached()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                CountPages++;
                var moviesList = await HubService.GetMoviesAsync(CountPages);
                var previousLastItem = moviesList.Last();
                foreach (var itemMovie in moviesList)
                {
                    itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                    ItemsMovie.Add(itemMovie);
                }
                Debug.WriteLine($"{ItemsMovie.Count()} {ItemsMovie.Count} ");

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
