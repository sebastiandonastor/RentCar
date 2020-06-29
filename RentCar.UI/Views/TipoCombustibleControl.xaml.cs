using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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
    public partial class TipoCombustibleControl : UserControl
    {

        private readonly IUnitOfWork _unitOfWork;

        public TipoCombustible TipoCombustibleSelected { get; set; } = new TipoCombustible();
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
                    await _unitOfWork.TiposCombustibles.AddAsync(TipoCombustibleSelected);
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
            descripcion.Text = "";
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
    }
}
