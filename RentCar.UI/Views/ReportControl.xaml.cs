using RentCar.UI.Reports;
using SAPBusinessObjects.WPF.ViewerShared;
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
    /// Interaction logic for ReportControl.xaml
    /// </summary>
    public partial class ReportControl : UserControl
    {
        public ReportControl()
        {
            InitializeComponent();
            this.Loaded += this.loaded;
        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            EmpleadosVentasReport rep = new EmpleadosVentasReport();
            rep.Load("./Reports/EmpleadosVentasReport.rpt");
            crystalReport.ViewerCore.ReportSource = rep;

        }
    }
}
