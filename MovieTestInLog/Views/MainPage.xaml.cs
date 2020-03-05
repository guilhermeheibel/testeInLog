using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieTestInLog.Models;
using MovieTestInLog.ViewModels;
using MovieTestInLog.Abstractions;
using MovieTestInLog.Services;

namespace MovieTestInLog.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
       public IHubServiceApi HubServiceMain { get; }
        public MainPage()
        {
            InitializeComponent();
            MasterBehavior = MasterBehavior.Popover;
            HubServiceMain = new HubServiceApi();
            Master = new MenuPage() { BindingContext = new MenuViewModel() };
            Detail = new NavigationPage(new MoviesPage() {BindingContext = new MoviesViewModel() { HubService = HubServiceMain } }) { BarBackgroundColor = Color.FromHex("#252525")};
        }

        protected override void OnAppearing()
        {
            
            base.OnAppearing();
        }
    }
}