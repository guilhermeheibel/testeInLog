using MovieTestInLog.Models;
using MovieTestInLog.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTestInLog.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MenuPage : ContentPage
    {
        private MenuViewModel ViewModel => BindingContext as MenuViewModel;
        
        public MenuPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}