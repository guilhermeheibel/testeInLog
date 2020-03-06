using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using MovieTestInLog.Models;
using MovieTestInLog.Services;
using System.Threading.Tasks;
using MovieTestInLog.Abstractions;
using MovieTestInLog.Views;
using MovieTestInLog.UI.Controls;

namespace MovieTestInLog.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public Command RefreshCommandPush { get; }
        public ConnectStatusControl StatusConnections = new ConnectStatusControl(); 
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
        public INavigation Navigation { get; set; }

        public IHubServiceApi HubService;
        public BaseViewModel()
        {
            RefreshCommandPush = new Command(async () => await ExecuteRefreshCommand());

            var masterBinding = Application.Current.MainPage as MainPage;
            if(masterBinding!=null)
            HubService = masterBinding.HubServiceMain;
        }
        public async Task ExecuteRefreshCommand()
        {
           
            await LoadAsync();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
            {
                return false;
            }

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        //LOAD VIEWMODELS GLOBAL
        public virtual Task LoadAsync()
        {

            return Task.FromResult(0);


        }
        //PUSH TYPES APP GLOBAL
        public async Task PushAsync<TViewModel>(params object[] args) where TViewModel : BaseViewModel
        {
            var viewModelType = typeof(TViewModel);
            var viewModelTypeName = viewModelType.Name;
            var viewModelWordLength = "ViewModel".Length;
            var viewTypeName = $"MovieTestInLog.Views.{viewModelTypeName.Substring(0, viewModelTypeName.Length - viewModelWordLength)}Page";
            var viewType = Type.GetType(viewTypeName);
            var page = Activator.CreateInstance(viewType) as Page;
            var viewModel = Activator.CreateInstance(viewModelType, args);
            if (page != null)
            {
                page.BindingContext = viewModel;
            }
            if(Application.Current.MainPage is MasterDetailPage masterMain)
            {
               await masterMain.Detail.Navigation.PushAsync(page);
            }
        }
        public async Task PushAsync<TViewModel>(Type t, params object[] args) where TViewModel : Page
        {
            var viewPageType = t;
            var viewPageTypeName = viewPageType.Name;
            var viewPageWordLength = "Page".Length;
            var viewTypeName = $"MovieTestInLog.ViewModels.{viewPageTypeName.Substring(0, viewPageTypeName.Length - viewPageWordLength)}ViewModel";
            var viewType = Type.GetType(viewTypeName);

             var page = Activator.CreateInstance(t) as Page;

            if (Activator.CreateInstance(viewType, args) is BaseViewModel viewModel)
            {
                page.BindingContext = viewModel;
            }

            if (Application.Current.MainPage is MasterDetailPage master)
            {
                await master.Detail.Navigation.PushAsync(page, false);
            }
        }


        #region INotifyPropertyChanged

        #endregion
    }
}
