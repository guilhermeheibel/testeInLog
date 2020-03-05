using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MovieTestInLog.Models
{
   public static class MenuModel
    {
       static public ObservableCollection<MenuItemsModel> All { get; }
        static MenuModel()
        {

            ObservableCollection<MenuItemsModel> menuMovies = new ObservableCollection<MenuItemsModel>{
               new MenuItemsModel("filmes", "","","",""), new MenuItemsModel("filmes", "","","","") , new MenuItemsModel("filmes", "","","",""), new MenuItemsModel("filmes", "","","",""), new MenuItemsModel("filmes", "","","",""), new MenuItemsModel("filmes", "","","","")};
            All = menuMovies;
        }
    }
}
