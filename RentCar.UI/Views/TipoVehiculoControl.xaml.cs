using MaterialDesignThemes.Wpf;
using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for TipoVehiculoControl.xaml
    /// </summary>
    public partial class TipoVehiculoControl : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public TipoVehiculo TipoVehiculoSelected { get; set; } = new TipoVehiculo();
        public TipoVehiculoControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            LoadData();
            this.DataContext = this;

        }


        void LoadData()
        {

            var tiposVehiculos = _unitOfWork.TiposVehiculos.GetAll();
            dataGrid.ItemsSource = tiposVehiculos.ToList();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TipoVehiculoSelected.Descripcion))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");

            }
            else
            {
                try
                {
                    await _unitOfWork.TiposVehiculos.AddAsync(TipoVehiculoSelected);
                    await _unitOfWork.CompleteAsync();

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {

                    cleanSelection();
                    LoadData();
                }

            }



        }

        void cleanSelection()
        {
            descripcion.Text = "";
            estados.SelectedIndex = -1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                TipoVehiculoSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

            }
        }

        private void OnClear(object sender, RoutedEventArgs e)
        {
            this.cleanSelection();
        }
    }
}
