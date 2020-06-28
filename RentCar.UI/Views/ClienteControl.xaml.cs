using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;


namespace RentCar.UI.Views
{
    /// <summary>
    /// Interaction logic for ClienteControl.xaml
    /// </summary>
    public partial class ClienteControl : UserControl
    {

        public ObservableCollection<Cliente> Clientes { get; set; } = new ObservableCollection<Cliente>();
        public ClienteControl()
        {
            InitializeComponent();
            this.DataContext = this;
            Clientes.Add(new Cliente() { Cedula = "00-1232132132-1", TipoPersona = new TipoPersona() { Id = 1, Tipo = "Fisico" }, Nombre = "Sebastian Doanstor" });


        }
    }
}
