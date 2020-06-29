using MaterialDesignThemes.Wpf;
using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.UI.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Media;

namespace RentCar.UI.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private IUnitOfWork _unitOfWork;

        public MainViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Load(MainWindow window)
        {

            var menuGestion = new List<SubItem>();
            menuGestion.Add(new SubItem("Clientes", PackIconKind.AccountCircle, new ClienteControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Empleados", PackIconKind.FaceAgent, new EmpleadoControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Marcas", PackIconKind.CarHatchback, new MarcaControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Modelo", PackIconKind.CarLimousine, new ModeloControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Tipos de Vehiculos", PackIconKind.CarMultiple, new TipoVehiculoControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Tipos de Combustible", PackIconKind.Fuel, new TipoCombustibleControl(_unitOfWork)));
            menuGestion.Add(new SubItem("Vehiculos", PackIconKind.Car, new VehiculoControler(_unitOfWork)));

            var item4 = new Item("Renta", new RentaControl(_unitOfWork), PackIconKind.CarRental);

            var item3 = new Item("Inspeccion", new InspeccionControl(_unitOfWork), PackIconKind.Magnify);

            var item2 = new Item("Gestion", menuGestion, PackIconKind.HomeEdit);

            var item1 = new Item("Inicio", new ClienteControl(_unitOfWork), PackIconKind.ViewDashboard);


            window.Menu.Children.Add(new MenuItemControl(item1, window));
            window.Menu.Children.Add(new MenuItemControl(item2, window));
            window.Menu.Children.Add(new MenuItemControl(item3, window));
            window.Menu.Children.Add(new MenuItemControl(item4, window));


        }




    }
}
