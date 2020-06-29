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
    /// Interaction logic for TipoCombustibleControl.xaml
    /// </summary>
    public partial class TipoCombustibleControl : UserControl, INotifyPropertyChanged
    {

        bool isEdit = false;

        private readonly IUnitOfWork _unitOfWork;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public TipoCombustible _tipoCombustible { get; set; }
        public TipoCombustible TipoCombustibleSelected
        {
            get { return _tipoCombustible; }
            set
            {
                _tipoCombustible = value;
                OnPropertyChanged();
                estados.SelectedIndex = value.Estado ? 0 : 1;
            }
        }
        public TipoCombustibleControl(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            InitializeComponent();
            LoadData();
            this.DataContext = this;

        }


        void LoadData()
        {

            var tiposCombustibles = _unitOfWork.TiposCombustibles.GetAll();
            dataGrid.ItemsSource = tiposCombustibles.ToList();

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TipoCombustibleSelected.Descripcion))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");

            }
            else
            {
                try
                {
                    if (isEdit)
                    {
                        var entity = await _unitOfWork.TiposCombustibles.GetAsync(TipoCombustibleSelected.Id);
                        _unitOfWork.TiposCombustibles.Update(entity, TipoCombustibleSelected);
                    }
                    else
                    {
                        await _unitOfWork.TiposCombustibles.AddAsync(TipoCombustibleSelected);
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
            TipoCombustibleSelected = new TipoCombustible();
            estados.SelectedIndex = -1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (estados.SelectedItem != null)
            {
                TipoCombustibleSelected.Estado = bool.Parse(((ComboBoxItem)estados.SelectedItem).Tag.ToString());

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
            var entity = await _unitOfWork.TiposCombustibles.GetAsync(id);
            TipoCombustibleSelected = entity;
        }

        private async void Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Seguro que desea Eliminar", "Eliminar", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                var id = int.Parse(((Button)sender).Tag.ToString());
                var entity = await _unitOfWork.TiposCombustibles.GetAsync(id);
                _unitOfWork.TiposCombustibles.Remove(entity);
                await _unitOfWork.CompleteAsync();

                this.LoadData();
            }

        }

    }
}
