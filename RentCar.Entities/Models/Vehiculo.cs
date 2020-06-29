using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public string Chasis { get; set; }

        public string NoMotor { get; set; }

        public string Placa { get; set; }

        public int IdTipoVehiculo { get; set; }

        public int IdMarca { get; set; }

        public int IdModelo { get; set; }

        public int IdTipoCombustible { get; set; }

        public bool Estado { get; set; }

        public TipoVehiculo TipoVehiculo { get; set; }

        public Marca Marca { get; set; }

        public Modelo Modelo { get; set; }

        
        public TipoCombustible TipoCombustible { get; set; }

        public List<Renta> Rentas { get; set; }
        public List<Inspeccion> Inspecciones { get; set; }


    }
}
