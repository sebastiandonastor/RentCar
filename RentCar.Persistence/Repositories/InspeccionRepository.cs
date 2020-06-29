using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories
{
    public class InspeccionRepository : BaseRepository<Inspeccion>, IInspeccionRepository
    {
        public InspeccionRepository(RentCarContext dbContext) : base(dbContext)
        {
        }
        public RentCarContext _context { get { return context; } }

        public void Update(Inspeccion oldInspeccion, Inspeccion newInspeccion)
        {
            oldInspeccion.Estado = newInspeccion.Estado;
            oldInspeccion.IdCliente = newInspeccion.IdCliente;
            oldInspeccion.IdEmpleado = newInspeccion.IdEmpleado;
            oldInspeccion.IdVehiculo = newInspeccion.IdVehiculo;
            oldInspeccion.TieneGato = newInspeccion.TieneGato;
            oldInspeccion.TieneGomaRespuesta = newInspeccion.TieneGomaRespuesta;
            oldInspeccion.TieneRalladura = newInspeccion.TieneRalladura;
            oldInspeccion.TieneRoturasCristal = newInspeccion.TieneRoturasCristal;
            oldInspeccion.Fecha = newInspeccion.Fecha;


        }
    }
}
