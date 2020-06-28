using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Inspeccion
    {
        public int Id { get; set; }

        public int IdVehiculo { get; set; }

        public int IdCliente { get; set; }

        public int IdEmpleado { get; set; }

        public bool TieneRalladura { get; set; }

        public string CantidadCombustible { get; set; }

        public bool TieneGomaRespuesta { get; set; }

        public bool TieneGato { get; set; }

        public bool TieneRoturasCristal { get; set; }

        public DateTime Fecha { get; set; }

        public bool Estado { get; set; }

        public Empleado Empleado { get; set; }

        public Cliente Cliente { get; set; }

        public Vehiculo Vehiculo { get; set; }

    }
}

