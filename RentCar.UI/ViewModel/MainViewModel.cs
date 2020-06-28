using MaterialDesignThemes.Wpf;
using RentCar.Entities.Models;
using RentCar.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace RentCar.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
        }

        public void Load(MainWindow window)
        {

            var menuGestion = new List<SubItem>();
            menuGestion.Add(new SubItem("Clientes", PackIconKind.AccountCircle, new TipoVehiculoControl()));
            menuGestion.Add(new SubItem("Empleados", PackIconKind.FaceAgent));
            menuGestion.Add(new SubItem("Marcas", PackIconKind.CarHatchback));
            menuGestion.Add(new SubItem("Modelo", PackIconKind.CarLimousine));
            menuGestion.Add(new SubItem("Tipos de Vehiculos", PackIconKind.CarMultiple));
            menuGestion.Add(new SubItem("Tipos de Combustible", PackIconKind.Fuel));
            menuGestion.Add(new SubItem("Vehiculos", PackIconKind.Car));

            var item3 = new Item("Inspeccion", new ClienteControl(), PackIconKind.Magnify);

            var item2 = new Item("Gestion", menuGestion, PackIconKind.HomeEdit);

            var item1 = new Item("Inicio", new ClienteControl(), PackIconKind.ViewDashboard);

            window.Menu.Children.Add(new MenuItemControl(item1, window));
            window.Menu.Children.Add(new MenuItemControl(item2, window));
            window.Menu.Children.Add(new MenuItemControl(item3, window));

        }




    }
}
