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
    public partial class SobreAppPage
    {
        private SobreAppViewModel ViewModel => BindingContext as SobreAppViewModel;

        public SobreAppPage()
        {
            InitializeComponent();
            BindingContext = ViewModel;
        }
    }
}