using MovieTestInLog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MovieTestInLog.Views
{
   public class BasePage : ContentPage
    {
        private BaseViewModel ViewModel => BindingContext as BaseViewModel;
        
        protected async override void OnAppearing()
        {
            if (ViewModel == null) return;
            ViewModel.IsBusy = true;
            Title = "";
            ViewModel.Navigation = this.Navigation;
            ViewModel.PropertyChanged += TitlePropertyChanged;
            await ViewModel.LoadAsync();
        }
        private void TitlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(ViewModel.Title)) return;

            Title = ViewModel.Title;
        }
    }
}
