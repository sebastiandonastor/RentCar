using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Empleado
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string TandaLabor { get; set; }

        public decimal PorcientoComision { get; set; }

        public DateTime FechaIngreso { get; set; }

        public bool Estado { get; set; }

        public List<Renta> Rentas { get; set; }

        public List<Inspeccion> Inspecciones { get; set; }

    }
}
