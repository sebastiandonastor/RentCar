using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
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

            var marcas = _unitOfWork.Marcas.GetAll();
            dataGrid.ItemsSource = marcas.ToList();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(MarcaSelected.Description))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");

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
