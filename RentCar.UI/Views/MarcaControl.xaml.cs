using MaterialDesignThemes.Wpf;
using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.UI.Validations;
using RentCar.UI.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
    /// Interaction logic for MarcaControl.xaml
    /// </summary>
    public partial class MarcaControl : UserControl, INotifyPropertyChanged
    {
        bool isEdit = false;
        private readonly IUnitOfWork _unitOfWork;
        private Marca _marca { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Marca MarcaSelected { get { return _marca; } set { _marca = value; OnPropertyChanged(); estados.SelectedIndex = value.Estado ? 0 : 1; } }
        public MarcaControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            LoadData();
            this.DataContext = this;

        }


        void LoadData()
        {
            this.cleanSelection();
            this.getPagination(1);
        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            var validation = new MarcaValidation();
            var result = validation.Validate(MarcaSelected);
            if (!result.IsValid)
            {

                MessageBox.Show(string.Join("<br/>", result.Errors.Select(r => r.ErrorMessage)), "Errores");

            }
            else
            {
                try
                {
                    if (isEdit)
                    {
                        var entity = await _unitOfWork.Marcas.GetAsync(MarcaSelected.Id);
                        _unitOfWork.Marcas.Updat(entity, MarcaSelected);
                    }
                    else
                    {
                        await _unitOfWork.Marcas.AddAsync(MarcaSelected);

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

        private void getPagination(int currentIndex)
        {
            var dataSource = _unitOfWork.Marcas.GetPaginatedCase((currentIndex - 1) * 5).ToList();
            dataGrid.ItemsSource = dataSource;
            buscadorCombox.ItemsSource = _unitOfWork.Marcas.GetPages(5);
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
                var dataSource = _unitOfWork.Marcas.GetPaginatedCase((id - 1) * 5, 5, (m => m.Description.Contains(busqueda)));
                dataGrid.ItemsSource = dataSource;

                buscadorCombox.ItemsSource = _unitOfWork.Marcas.GetPages(5, (m => m.Description.Contains(busqueda)));

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



        void cleanSelection()
        {
            isEdit = false;
            MarcaSelected = new Marca();
            descripcion.Text = "";
            estados.SelectedIndex = -1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                MarcaSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

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
            var entity = await _unitOfWork.Marcas.GetAsync(id);
            MarcaSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Marcas.GetAsync(id);
                _unitOfWork.Marcas.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData();
            }

        }
    }
}
