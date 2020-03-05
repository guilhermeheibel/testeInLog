using MovieTestInLog.Models;
using MovieTestInLog.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieTestInLog.ViewModels
{
   
   public class MenuViewModel : BaseViewModel
    {
      public  ObservableCollection<MenuItemsModel> menu { get; }
      public ICommand ShowPagesMenuSelected { get; }
        public MenuViewModel()
        {
            menu = MenuModel.All;
            ShowPagesMenuSelected = new Command<MenuItemsModel>(async (x) => await ExecutePageSelected(x));

        }
        private async Task ExecutePageSelected(MenuItemsModel typePageSelected)
        {
            if (Application.Current.MainPage is MasterDetailPage master)
            {
                if(master.Detail.Navigation.NavigationStack.LastOrDefault().GetType() == typePageSelected.ItemPage){ return; }
                master.IsPresented = false;
               await PushAsync<BasePage>(typePageSelected.ItemPage);
            
            }
        }

        public override async Task LoadAsync()
        {

        }
    }
}
