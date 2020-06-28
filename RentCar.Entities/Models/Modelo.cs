using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Modelo
    {
        public int Id { get; set; }

        public int IdMarca { get; set; }

        public string Descripcion { get; set; }

        public bool Estado { get; set; }

        public Marca Marca { get; set; }

        public List<Vehiculo> Vehiculos { get; set; }

    }
}
