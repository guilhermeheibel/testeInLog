using MovieTestInLog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MovieTestInLog.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesDetailPage 
    {
        private MoviesDetailViewModel ViewModel => BindingContext as MoviesDetailViewModel;

        public MoviesDetailPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}