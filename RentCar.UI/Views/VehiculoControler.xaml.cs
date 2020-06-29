using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for VehiculoControler.xaml
    /// </summary>
    public partial class VehiculoControler : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public Vehiculo VehiculoSelected { get; set; } = new Vehiculo();


        public VehiculoControler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += LoadData;

        }


        void LoadData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _unitOfWork.Vehiculos.GetAll();
            marcaCombobox.ItemsSource = _unitOfWork.Marcas.GetAll();
            tipoVehiculoCombox.ItemsSource = _unitOfWork.TiposVehiculos.GetAll();
            tipoCombustibleCombox.ItemsSource = _unitOfWork.TiposCombustibles.GetAll();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(VehiculoSelected.Descripcion))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");
            }
            else
            {
                try
                {

                    await _unitOfWork.Vehiculos.AddAsync(VehiculoSelected);
                    await _unitOfWork.CompleteAsync();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                    cleanSelection();
                    LoadData(sender, e);
                }

            }


        }

        void cleanSelection()
        {
            VehiculoSelected = new Vehiculo();
            descripcion.Text = "";
            placa.Text = "";
            estados.SelectedIndex = -1;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                VehiculoSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

            }
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            this.cleanSelection();
        }

        public void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void marcaCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (marcaCombobox.SelectedItem != null)
            {
                var id = ((Marca)marcaCombobox.SelectedItem).Id;

                modeloCombobox.ItemsSource = _unitOfWork.Modelos.GetAll(m => m.IdMarca == id);

            }


        }
    }
}

