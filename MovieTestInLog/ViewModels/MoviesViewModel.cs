﻿using System.Threading.Tasks;
using Xamarin.Forms;
using MovieTestInLog.Models;
using System.Windows.Input;
using MovieTestInLog.UI.Utils;
using Xamarin.Forms.Extended;

namespace MovieTestInLog.ViewModels
{
    public class MoviesViewModel : BaseViewModel
    {
        public InfiniteScrollCollection<MoviesModel> ItemsMovie { get; }
        private int CountPages = 1;
        bool SearchInit = false;
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

                    if (moviesList == null) return new InfiniteScrollCollection<MoviesModel>();
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
            SearchInit = true;
            IsBusy = true;
            ItemsMovie.Clear();
            if (!await StatusConnections.VerifyConnect()) {
                
                ItemsMovie.Add(new MoviesModel() { title = "Sem conexão ativa com a internet..." } );
                return;
            }
            ItemsMovie.Clear();
            var moviesList = await HubService.GetMoviesAsync(1);
            if (moviesList == null) return;
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
              if(SearchInit)
                    ExecuteSearchCommand();

            }
        }
        private async void ExecuteSearchCommand()
        {
           
            ItemsMovie.Clear();

            if (!await StatusConnections.VerifyConnect())
            {

                ItemsMovie.Add(new MoviesModel() { title = "Sem conexão ativa com a internet..." });
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
            MovieSearchModel movies = await HubService.GetSearchMovieAsync(SearchText, CountPages.ToString());
            if (movies == null) return;

            

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