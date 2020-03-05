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

        public int CountPages
        {
            get { return _countPages; }
            set { SetProperty(ref _countPages, value); }
        }
        public CarrouselMoviesViewModel()
        {
            Title = "Movies List";
            ItemsMovie = new InfiniteScrollCollection<MoviesModel>();
            ItemTresholdReachedCommand = new Command(async () => await ItemsTresholdReached());
            ShowMovieDetailCommand = new Command<MoviesModel>(async (x) => await ExecuteMovieDetail(x));
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
           
            var movies = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());

            if (CountPages == 1)
                ItemsMovie.Clear();
            foreach (var itemMovie in movies.results)
            {
                itemMovie.poster_path = PathMoviesImage.PathConverter(itemMovie.id.ToString(), itemMovie.poster_path);

                ItemsMovie.Add(itemMovie
                    );
            }
            OnPropertyChanged(nameof(ItemsMovie));
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
