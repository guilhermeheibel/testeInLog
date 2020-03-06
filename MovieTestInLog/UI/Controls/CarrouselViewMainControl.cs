using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieTestInLog.UI.Controls
{
    class CollectionViewMainControl : CollectionView
    {
        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create("ItemTappedCommand", typeof(ICommand), typeof(CollectionViewMainControl), null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }
       

        private void Initialize()
        {
            this.SelectionChanged += (sender, e) =>
            {
                if (ItemTappedCommand == null)
                    return;
                try
                {
                    ItemTappedCommand.Execute(e.CurrentSelection);

                }
                catch (Exception ex) { Debug.WriteLine("ERRO999 CLICK ITEMTAPPED LISTVIEWMAINCONTROL: :" + ex.Message); return; }
            };
        }

        public CollectionViewMainControl()
        {
            Initialize();
        }

    }
}
