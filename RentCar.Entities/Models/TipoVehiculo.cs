using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class TipoVehiculo
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public List<Vehiculo> Vehiculos { get; set; }
    }
}
