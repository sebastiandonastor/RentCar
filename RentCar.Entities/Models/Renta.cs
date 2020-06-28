using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Renta
    {

        public int Id { get; set; }

        public int IdEmpleado { get; set; }

        public int IdVehiculo { get; set; }

        public int IdCliente { get; set; }

        public DateTime FechaRenta { get; set; }

        public DateTime? FechaDevolucion { get; set; }

        public decimal MontoDiario { get; set; }

        public int CantidadDias { get; set; }

        public string Comentario { get; set; }

        public int Estado { get; set; }

        public Vehiculo Vehiculo { get; set; }
        public Cliente Cliente { get; set; }

        public Empleado Empleado { get; set; }

    }
}
