using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public string NoTarjetaCr { get; set; }

        public int LimiteCredito { get; set; }

        public int IdTipoPersona { get; set; }

        public TipoPersona TipoPersona { get; set; }

        public bool Estado { get; set; }

        public List<Renta> Rentas { get; set; }

        public List<Inspeccion> Inspecciones { get; set; }



    }
}
