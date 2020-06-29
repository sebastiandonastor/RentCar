using MaterialDesignThemes.Wpf;
using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for TipoVehiculoControl.xaml
    /// </summary>
    public partial class TipoVehiculoControl : UserControl, INotifyPropertyChanged
    {
        bool isEdit = false;

        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TipoVehiculo _TipoVehiculo { get; set; }
        public TipoVehiculo TipoVehiculoSelected
        {
            get { return _TipoVehiculo; }
            set
            {
                _TipoVehiculo = value;
                OnPropertyChanged();
                estados.SelectedIndex = value.Estado ? 0 : 1;

            }
        }
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
                    if (isEdit)
                    {
                        var entity = await _unitOfWork.TiposVehiculos.GetAsync(TipoVehiculoSelected.Id);
                        _unitOfWork.TiposVehiculos.Update(entity, TipoVehiculoSelected);
                    }
                    else
                    {
                        await _unitOfWork.TiposVehiculos.AddAsync(TipoVehiculoSelected);

                    }
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
            isEdit = false;
            TipoVehiculoSelected = new TipoVehiculo();
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

        private async void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            isEdit = true;
            var id = int.Parse(((Button)sender).Tag.ToString());
            var entity = await _unitOfWork.TiposVehiculos.GetAsync(id);
            TipoVehiculoSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.TiposVehiculos.GetAsync(id);
                _unitOfWork.TiposVehiculos.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData();
            }

        }
    }
}
