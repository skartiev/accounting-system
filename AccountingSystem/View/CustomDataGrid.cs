using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.View
{
    internal class CustomDataGrid : DataGrid
    {
        public CustomDataGrid()
        {
            SelectionChanged += CustomDataGrid_SelectionChanged;
        }

        private void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = SelectedItems.OfType<User>().ToList(); //this.SelectedItems;
        }

        // FIXME: remove reference to User
        public IList<User> SelectedItemsList
        {
            get
            {
                return (IList<User>)GetValue(SelectedItemsListProperty); 
            }

            set 
            {
                try
                {
                    SetValue(SelectedItemsListProperty, value);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public static readonly DependencyProperty SelectedItemsListProperty = DependencyProperty.Register("SelectedItemsList", typeof(IList<User>), typeof(CustomDataGrid), new PropertyMetadata(null));
       
    }
}
