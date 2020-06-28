using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Marca
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool Estado { get; set; }

        public List<Modelo> Modelos { get; set; }

        public List<Vehiculo> Vehiculos { get; set; }
    }
}
