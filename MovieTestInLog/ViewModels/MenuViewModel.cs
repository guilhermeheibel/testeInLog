using MovieTestInLog.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestInLog.ViewModels
{
   
   public class MenuViewModel : BaseViewModel
    {
      public  ObservableCollection<MenuItemsModel> menu { get; }
        
        public MenuViewModel()
        {
            menu = MenuModel.All;
          
        }

        public override async Task LoadAsync()
        {

        }
    }
}
