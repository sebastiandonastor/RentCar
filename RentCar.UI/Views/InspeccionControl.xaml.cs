using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for InspeccionControl.xaml
    /// </summary>
    public partial class InspeccionControl : UserControl, INotifyPropertyChanged
    {

        public bool IsEdit = false;
        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Inspeccion _inspeccion { get; set; }
        public Inspeccion InspeccionSelected
        {
            get { return _inspeccion; }
            set
            {
                _inspeccion = value; OnPropertyChanged(); estados.SelectedIndex = value.Estado ? 0 : 1;
            }
        }


        public InspeccionControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += LoadData;

        }


        void LoadData(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = _unitOfWork.Inspecciones.GetAll();
            vehiculoCombox.ItemsSource = _unitOfWork.Vehiculos.GetAll();
            clienteCombox.ItemsSource = _unitOfWork.Clientes.GetAll();
            empleadoCombox.ItemsSource = _unitOfWork.Empleados.GetAll();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {

            try
            {
                if (IsEdit)
                {
                    var oldEntity = await _unitOfWork.Inspecciones.GetAsync(InspeccionSelected.Id);
                    _unitOfWork.Inspecciones.Update(oldEntity, InspeccionSelected);
                    await _unitOfWork.CompleteAsync();
                }
                else
                {

                    await _unitOfWork.Inspecciones.AddAsync(InspeccionSelected);
                    await _unitOfWork.CompleteAsync();
                }
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

        void cleanSelection()
        {
            this.IsEdit = false;

            InspeccionSelected = new Inspeccion() { Fecha = DateTime.Now };
            estados.SelectedIndex = -1;
            empleadoCombox.SelectedIndex = 0;
            empleadoCombox.Text = null;

            vehiculoCombox.SelectedIndex = 0;
            vehiculoCombox.Text = null;

            clienteCombox.SelectedIndex = 0;
            clienteCombox.Text = null;

            tieneGato.IsChecked = false;
            tieneGomaRespuesta.IsChecked = false;
            tieneRalladura.IsChecked = false;
            tieneRoturaCristal.IsChecked = false;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                InspeccionSelected.Estado = ((ComboBoxItem)estados.SelectedItem).Content.ToString() == "Activo";

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

        private void cantidadCombustible_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cantidadCombustible.SelectedItem != null)
            {
                InspeccionSelected.CantidadCombustible = ((ComboBoxItem)cantidadCombustible.SelectedItem).Content.ToString();
            }
        }

        private async void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            this.IsEdit = true;
            var id = int.Parse(((Button)sender).Tag.ToString());
            var entity = await _unitOfWork.Inspecciones.GetAsync(id);
            InspeccionSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Inspecciones.GetAsync(id);
                _unitOfWork.Inspecciones.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }
    }
}
