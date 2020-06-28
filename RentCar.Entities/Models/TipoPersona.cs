using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Entities.Models
{
    public class TipoPersona
    {
        public int Id { get; set; }

        public string Tipo { get; set; }

        public List<Cliente> Clientes { get; set; }
    }
}
