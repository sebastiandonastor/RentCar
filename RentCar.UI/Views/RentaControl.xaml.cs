using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RentaControl.xaml
    /// </summary>
    public partial class RentaControl : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public Renta RentaSelected { get; set; } = new Renta() { FechaRenta = DateTime.Now };


        public RentaControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += LoadData;

        }


        void LoadData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _unitOfWork.Rentas.GetAll();
            vehiculoCombox.ItemsSource = _unitOfWork.Vehiculos.GetAll();
            clienteCombox.ItemsSource = _unitOfWork.Clientes.GetAll();
            empleadoCombox.ItemsSource = _unitOfWork.Empleados.GetAll();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (RentaSelected.MontoDiario <= 0)
            {
                MessageBox.Show("Por favor ingrese una Monto Diario valida", "Error");
            }
            else
            {
                try
                {

                    RentaSelected.FechaDevolucion = DateTime.Parse(fechaDevolucion.Text);
                    await _unitOfWork.Rentas.AddAsync(RentaSelected);
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
            RentaSelected = new Renta();
            montoDiario.Text = "";
            cantidadDias.Text = "";
            estados.SelectedIndex = -1;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                RentaSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString()) ? 1 : 0;

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


    }
}
