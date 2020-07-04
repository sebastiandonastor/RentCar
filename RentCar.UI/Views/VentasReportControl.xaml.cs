using RentCar.UI.Reports;
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
    /// Interaction logic for VentasReportControl.xaml
    /// </summary>
    public partial class VentasReportControl : UserControl
    {
        public VentasReportControl()
        {
            InitializeComponent();
            this.Loaded += this.loaded;
        }

        private void loaded(object sender, RoutedEventArgs e)
        {
            VehiculosVentasReport rep = new VehiculosVentasReport();
            rep.Load("./Reports/VehiculosVentasReport.rpt");
            crystalReport.ViewerCore.ReportSource = rep;

        }
    }
}
