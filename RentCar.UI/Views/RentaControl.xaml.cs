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
    /// Interaction logic for RentaControl.xaml
    /// </summary>
    public partial class RentaControl : UserControl, INotifyPropertyChanged
    {
        bool isEdit = false;

        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private Renta _renta { get; set; }
        public Renta RentaSelected
        {
            get { return _renta; }
            set
            {
                _renta = value;
                OnPropertyChanged();
                estados.SelectedIndex = value.Estado;
            }
        }


        public RentaControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += LoadData;

        }


        void LoadData(object sender, RoutedEventArgs e)
        {
            this.cleanSelection();
            this.getPagination(1);
            vehiculoCombox.ItemsSource = _unitOfWork.Vehiculos.GetAll(v => !v.Rentas.Any(r => r.FechaDevolucion == null));
            clienteCombox.ItemsSource = _unitOfWork.Clientes.GetAll();
            empleadoCombox.ItemsSource = _unitOfWork.Empleados.GetAll();

        }

        private void getPagination(int currentIndex)
        {
            var dataSource = _unitOfWork.Rentas.GetPaginatedCase((currentIndex - 1) * 5).ToList();
            dataGrid.ItemsSource = dataSource;
            buscadorCombox.ItemsSource = _unitOfWork.Vehiculos.GetPages(5);
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
                var dataSource = _unitOfWork.Rentas.GetPaginatedCase((id - 1) * 5, 5,
                    r => r.Comentario.Contains(busqueda) || r.Empleado.Nombre.Contains(busqueda) || r.Vehiculo.Descripcion.Contains(busqueda)).ToList();
                dataGrid.ItemsSource = dataSource;

                buscadorCombox.ItemsSource = _unitOfWork.Rentas.GetPages(5,
                    r => r.Comentario.Contains(busqueda) || r.Empleado.Nombre.Contains(busqueda) || r.Vehiculo.Descripcion.Contains(busqueda));

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

        private async void onSave(object sender, RoutedEventArgs e)
        {

            var validation = new RentaValidation();
            var result = validation.Validate(RentaSelected);
            if (!result.IsValid)
            {

                MessageBox.Show(string.Join("\n", result.Errors.Select(r => r.ErrorMessage)), "Errores");

            }
            else
            {
                try
                {

                    if (isEdit)
                    {
                        var entity = await _unitOfWork.Rentas.GetAsync(RentaSelected.Id);
                        _unitOfWork.Rentas.Update(entity, RentaSelected);
                    }
                    else
                    {
                        await _unitOfWork.Rentas.AddAsync(RentaSelected);

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
            RentaSelected = new Renta() { FechaRenta = DateTime.Now };
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

        private async void Edit_Button_Click(object sender, RoutedEventArgs e)
        {
            isEdit = true;
            var id = int.Parse(((Button)sender).Tag.ToString());
            var entity = await _unitOfWork.Rentas.GetAsync(id);
            var vehiculos = _unitOfWork.Vehiculos.GetAll(v => !v.Rentas.Any(r => r.FechaDevolucion == null)).ToList();
            if (vehiculos.Count(v => v.Id == entity.Vehiculo.Id) == 0)
                vehiculos.Add(entity.Vehiculo);

            vehiculoCombox.ItemsSource = vehiculos;
            RentaSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Rentas.GetAsync(id);
                _unitOfWork.Rentas.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }


    }
}
