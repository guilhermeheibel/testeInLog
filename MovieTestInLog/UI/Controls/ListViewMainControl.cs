using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MovieTestInLog.UI.Controls
{
    public class ListViewMainControl : ListView
    {
        public static readonly BindableProperty ItemTappedCommandProperty = BindableProperty.Create("ItemTappedCommand", typeof(ICommand), typeof(ListViewMainControl), null);
        public ICommand ItemTappedCommand
        {
            get { return (ICommand)GetValue(ItemTappedCommandProperty); }
            set
            {
                SetValue(ItemTappedCommandProperty, value);
            }
        }
        public ListViewMainControl(ListViewCachingStrategy strategy) : base(strategy)
        {
            //IsEnabled = true;
            Initialize();
        }

        private void Initialize()
        {
            this.ItemSelected += (sender, e) =>
            {
                if (ItemTappedCommand == null)
                    return;
                try
                {/* IsEnabled = false;*/
                    // if (ItemTappedCommand.CanExecute(e.SelectedItem))
                    ItemTappedCommand.Execute(e.SelectedItem);

                }
                catch (Exception ex) { Debug.WriteLine("ERRO999 CLICK ITEMTAPPED LISTVIEWMAINCONTROL: :" + ex.Message); return; }
            };
        }

        public ListViewMainControl()
        {
            Initialize();
        }

    }
}
