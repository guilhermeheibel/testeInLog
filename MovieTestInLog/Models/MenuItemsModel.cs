﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieTestInLog.Models
{
    public class MenuItemsModel
    {
        public MenuItemsModel(string itemName, string itemIcon, string itemDescription, string itemBackgroudColor, string itemTextColor, Type itemPage)
        {
            ItemName = itemName;
            ItemIcon = itemIcon;
            ItemDescription = itemDescription;
            ItemBackgroudColor = itemBackgroudColor;
            ItemTextColor = itemTextColor;
            ItemPage = itemPage;
        }
        public MenuItemsModel()
        {

        }
        public string ItemName { get; set; }
        public string ItemIcon { get; set; }
        public string ItemDescription { get; set; }
        public string ItemBackgroudColor { get; set; }
        public string ItemTextColor { get; set; }
        public Type ItemPage { get; set; }
       
    }
}
