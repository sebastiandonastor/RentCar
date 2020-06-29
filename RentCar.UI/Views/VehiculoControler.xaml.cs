using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class VehiculoControler : UserControl, INotifyPropertyChanged
    {
        bool isEdit = false;
        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Vehiculo _vehiculo { get; set; }
        public Vehiculo VehiculoSelected
        {
            get { return _vehiculo; }
            set
            {
                _vehiculo = value;
                OnPropertyChanged();
                estados.SelectedIndex = value.Estado ? 0 : 1;
            }
        }


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
                    if (isEdit)
                    {
                        var entity = await _unitOfWork.Vehiculos.GetAsync(VehiculoSelected.Id);
                        _unitOfWork.Vehiculos.Update(entity, VehiculoSelected);
                    }
                    else
                    {
                        await _unitOfWork.Vehiculos.AddAsync(VehiculoSelected);

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
                    LoadData(sender, e);
                }

            }


        }

        void cleanSelection()
        {
            isEdit = false;
            VehiculoSelected = new Vehiculo();
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

        private async void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            isEdit = true;
            var id = int.Parse(((Button)sender).Tag.ToString());
            var entity = await _unitOfWork.Vehiculos.GetAsync(id);
            VehiculoSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Vehiculos.GetAsync(id);
                _unitOfWork.Vehiculos.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }

    }
}

