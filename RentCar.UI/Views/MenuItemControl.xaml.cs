using RentCar.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for MenuItemControl.xaml
    /// </summary>
    public partial class MenuItemControl : UserControl
    {

        MainWindow _context;

        public MenuItemControl(Item item, MainWindow context)
        {
            InitializeComponent();

            ExpanderMenu.Visibility = item.SubItems == null ? Visibility.Collapsed : Visibility.Visible;
            ListViewItemMenu.Visibility = item.SubItems == null ? Visibility.Visible : Visibility.Collapsed;
            _context = context;
            this.DataContext = item;
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _context.SwitchScreen(((SubItem)((ListView)sender).SelectedItem).Screen);
        }


        private void MouseUp_Selection(object sender, MouseButtonEventArgs e)
        {

            var screen = ((Item)((ListBoxItem)sender).DataContext).Screen;
            if (screen != null)
            {
                _context.SwitchScreen(screen);
            }
        }
    }
}
