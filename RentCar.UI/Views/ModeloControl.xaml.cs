﻿using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.UI.Validations;
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
    /// Interaction logic for ModeloControl.xaml
    /// </summary>
    public partial class ModeloControl : UserControl, INotifyPropertyChanged
    {
        Boolean isEdit = false;

        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Modelo _modelo { get; set; }
        public Modelo ModeloSelected
        {
            get { return _modelo; }
            set { _modelo = value; OnPropertyChanged(); estados.SelectedIndex = value.Estado ? 0 : 1; }
        }

        public List<Marca> Marcas { get; set; } = new List<Marca>();

        public ModeloControl(IUnitOfWork unitOfWork)
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

            Marcas = _unitOfWork.Marcas.GetAll().ToList();
            marcaCombox.ItemsSource = Marcas;

        }

        private void getPagination(int currentIndex)
        {
            var dataSource = _unitOfWork.Modelos.GetPaginatedCase((currentIndex - 1) * 5).ToList();
            dataGrid.ItemsSource = dataSource;
            buscadorCombox.ItemsSource = _unitOfWork.Modelos.GetPages(5);
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
                var dataSource = _unitOfWork.Modelos.GetPaginatedCase((id - 1) * 5, 5,
                    m => m.Descripcion.Contains(busqueda) || m.Marca.Description.Contains(busqueda)).ToList();
                dataGrid.ItemsSource = dataSource;

                buscadorCombox.ItemsSource = _unitOfWork.Vehiculos.GetPages(5,
                    m => m.Descripcion.Contains(busqueda) || m.Marca.Description.Contains(busqueda)).ToList();

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

            var validation = new ModeloValidation();
            var result = validation.Validate(ModeloSelected);
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
                        var entity = await _unitOfWork.Modelos.GetAsync(ModeloSelected.Id);
                        _unitOfWork.Modelos.Update(entity, ModeloSelected);
                    }
                    else
                    {
                        await _unitOfWork.Modelos.AddAsync(ModeloSelected);

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
            ModeloSelected = new Modelo();
            estados.SelectedIndex = -1;


        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                ModeloSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

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
            var entity = await _unitOfWork.Modelos.GetAsync(id);
            ModeloSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Modelos.GetAsync(id);
                _unitOfWork.Modelos.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }
    }
}
