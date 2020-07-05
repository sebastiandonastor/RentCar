using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.UI.Validations;
using RentCar.UI.ViewModel;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for EmpleadoControl.xaml
    /// </summary>
    public partial class EmpleadoControl : UserControl, INotifyPropertyChanged
    {
        bool isEdit = false;

        private readonly IUnitOfWork _unitOfWork;

        private Empleado _empleado { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Empleado EmpleadoSelected { get { return _empleado; } set { _empleado = value; OnPropertyChanged(); estados.SelectedIndex = value.Estado ? 0 : 1; } }


        public EmpleadoControl(IUnitOfWork unitOfWork)
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

        }

        private void getPagination(int currentIndex)
        {
            var dataSource = _unitOfWork.Empleados.GetPaginatedCase((currentIndex - 1) * 5).ToList();
            dataGrid.ItemsSource = dataSource;
            buscadorCombox.ItemsSource = _unitOfWork.Empleados.GetPages(5);
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
                var dataSource = _unitOfWork.Empleados.GetPaginatedCase((id - 1) * 5, 5,
                    em => em.Nombre.Contains(busqueda) || em.Cedula.Contains(busqueda)).ToList();
                dataGrid.ItemsSource = dataSource;

                buscadorCombox.ItemsSource = _unitOfWork.Empleados.GetPages(5,
                    em => em.Nombre.Contains(busqueda) || em.Cedula.Contains(busqueda)).ToList();

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

            var validation = new EmpleadoValidation();
            var result = validation.Validate(EmpleadoSelected);
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
                        var entity = await _unitOfWork.Empleados.GetAsync(EmpleadoSelected.Id);
                        _unitOfWork.Empleados.Update(entity, EmpleadoSelected);
                    }
                    else
                    {
                        await _unitOfWork.Empleados.AddAsync(EmpleadoSelected);
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
            EmpleadoSelected = new Empleado() { FechaIngreso = DateTime.Now };
            estados.SelectedIndex = -1;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                EmpleadoSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

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
            var entity = await _unitOfWork.Empleados.GetAsync(id);
            EmpleadoSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Empleados.GetAsync(id);
                _unitOfWork.Empleados.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }
    }
}
