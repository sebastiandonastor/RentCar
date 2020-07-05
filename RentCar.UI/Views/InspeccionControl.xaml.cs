using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.UI.Validations;
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

        private void getPagination(int currentIndex)
        {
            var dataSource = _unitOfWork.Inspecciones.GetPaginatedCase((currentIndex - 1) * 5).ToList();
            dataGrid.ItemsSource = dataSource;
            buscadorCombox.ItemsSource = _unitOfWork.Inspecciones.GetPages(5);
            buscadorCombox.SelectedItem = currentIndex;
        }



        private void Buscar_TextInput(object sender, KeyEventArgs e)
        {
            var busqueda = this.buscador.Text;
            var id = ((int)buscadorCombox.SelectedItem);

            if (String.IsNullOrWhiteSpace(busqueda))
            {
                this.getPagination(1);
            }
            else
            {
                var dataSource = _unitOfWork.Inspecciones.GetPaginatedCase((id - 1) * 5, 5,
                    (i => i.Empleado.Nombre.Contains(busqueda) || i.Vehiculo.Descripcion.Contains(busqueda) || i.Cliente.Nombre.Contains(busqueda))).ToList();
                dataGrid.ItemsSource = dataSource;

                buscadorCombox.ItemsSource = _unitOfWork.Inspecciones.GetPages(5,
                    (i => i.Empleado.Nombre.Contains(busqueda) || i.Vehiculo.Descripcion.Contains(busqueda) || i.Cliente.Nombre.Contains(busqueda)));

            }
        }

        private void buscadorCombox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (buscadorCombox.SelectedItem != null)
            {
                var id = ((int)buscadorCombox.SelectedItem);
                this.getPagination(id);

            }
        }




        void LoadData(object sender, RoutedEventArgs e)
        {
            this.cleanSelection();
            this.getPagination(1);
            vehiculoCombox.ItemsSource = _unitOfWork.Vehiculos.GetAll();
            clienteCombox.ItemsSource = _unitOfWork.Clientes.GetAll();
            empleadoCombox.ItemsSource = _unitOfWork.Empleados.GetAll();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {

            var validation = new InspeccionValidation();
            var result = validation.Validate(InspeccionSelected);
            if (!result.IsValid)
            {

                MessageBox.Show(string.Join("\n", result.Errors.Select(r => r.ErrorMessage)), "Errores");

            }
            else
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





        }

        void cleanSelection()
        {
            this.IsEdit = false;
            InspeccionSelected = new Inspeccion() { Fecha = DateTime.Now };
            estados.SelectedIndex = -1;

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
