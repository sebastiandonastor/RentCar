﻿using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for ClienteControl.xaml
    /// </summary>
    public partial class ClienteControl : UserControl
    {


        private readonly IUnitOfWork _unitOfWork;

        public Cliente ClienteSelected { get; set; } = new Cliente();

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

                    await _unitOfWork.Clientes.AddAsync(ClienteSelected);
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
            ClienteSelected = new Cliente();
            nombre.Text = "";
            cedula.Text = "";
            tarjetaCredito.Text = "";
            limiteCredito.Text = "";

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
    }
}
