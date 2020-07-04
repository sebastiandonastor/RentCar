using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for ClienteControl.xaml
    /// </summary>
    public partial class ClienteControl : UserControl, INotifyPropertyChanged
    {

        bool isEdit = false;
        private Cliente _cliente { get; set; }
        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public Cliente ClienteSelected { get { return _cliente; } set { _cliente = value; OnPropertyChanged(); estados.SelectedIndex = value.Estado ? 0 : 1; } }

        public List<TipoPersona> TiposPersonas { get; set; } = new List<TipoPersona>();

        public ClienteControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += LoadData;

        }


        void LoadData(object sender, RoutedEventArgs e)
        {
            this.cleanSelection();
            dataGrid.ItemsSource = _unitOfWork.Clientes.GetAll();
            tipoPersonaCombox.ItemsSource = _unitOfWork.TiposPersonas.GetAll();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ClienteSelected.Nombre))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");
            }
            else
            {
                try
                {
                    if (isEdit)
                    {
                        var entity = await _unitOfWork.Clientes.GetAsync(ClienteSelected.Id);
                        _unitOfWork.Clientes.Update(entity, ClienteSelected);
                    }
                    else
                    {
                        await _unitOfWork.Clientes.AddAsync(ClienteSelected);

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
            ClienteSelected = new Cliente();

            estados.SelectedIndex = -1;

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                ClienteSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

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
            var entity = await _unitOfWork.Clientes.GetAsync(id);
            ClienteSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.Clientes.GetAsync(id);
                _unitOfWork.Clientes.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData(sender, e);
            }

        }

    }
}
