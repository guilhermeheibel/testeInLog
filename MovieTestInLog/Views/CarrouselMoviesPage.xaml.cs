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
    public partial class CarrouselMoviesPage
    {
        private CarrouselMoviesViewModel ViewModel => BindingContext as CarrouselMoviesViewModel;
        public CarrouselMoviesPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}