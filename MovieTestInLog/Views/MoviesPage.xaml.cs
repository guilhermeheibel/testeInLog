using System.ComponentModel;
using MovieTestInLog.ViewModels;

namespace MovieTestInLog.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MoviesPage 
    {
        private MoviesViewModel ViewModel => BindingContext as MoviesViewModel;

        public MoviesPage()
        {
            InitializeComponent();
           BindingContext = ViewModel;
        }

    }
}