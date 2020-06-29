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
    /// Interaction logic for ModeloControl.xaml
    /// </summary>
    public partial class ModeloControl : UserControl
    {
        private readonly IUnitOfWork _unitOfWork;

        public Modelo ModeloSelected { get; set; } = new Modelo();

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
            dataGrid.ItemsSource = _unitOfWork.Modelos.GetModelosWithMarcas().ToList();

            Marcas = _unitOfWork.Marcas.GetAll().ToList();
            marcaCombox.ItemsSource = Marcas;

        }

        private async void onSave(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(ModeloSelected.Descripcion))
            {
                MessageBox.Show("Por favor ingrese una descripcion valida", "Error");
            }
            else
            {
                try
                {
                    await _unitOfWork.Modelos.AddAsync(ModeloSelected);
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
            descripcion.Text = "";
            estados.SelectedIndex = -1;
            marcaCombox.SelectedIndex = 0;

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
    }
}
