using System;

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

        public bool EstadoGomaDelanteraDerecha { get; set; }

        public bool EstadoGomaDelanteraIzquierda { get; set; }


        public bool EstadoGomaTraseraDerecha { get; set; }

        public bool EstadoGomaTraseraIzquierda { get; set; }

        public bool TieneGato { get; set; }

        public bool TieneRoturasCristal { get; set; }

        public DateTime Fecha { get; set; }

        public bool Estado { get; set; }

        public Empleado Empleado { get; set; }

        public Cliente Cliente { get; set; }

        public Vehiculo Vehiculo { get; set; }

    }
}

