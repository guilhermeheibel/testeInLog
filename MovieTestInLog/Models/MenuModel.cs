using MovieTestInLog.Views;
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
                new MenuItemsModel("Normal List", "normalList.svg","Lista normal com scroll em vertical","#00447C","#FFFFFF", typeof(MoviesPage)),
                new MenuItemsModel("Carrousel List", "carrouselList.svg","Lista diferenciada em horiontal","#0D959F","#FFFFFF",  typeof(CarrouselMoviesPage)) , 
                new MenuItemsModel("Sobre o App", "sobre.svg","Informaçoes do aplicativo de teste.","#252525","#FFFFFF",  typeof(SobreAppPage))
                };
                All = menuMovies;
        }
    }
}
