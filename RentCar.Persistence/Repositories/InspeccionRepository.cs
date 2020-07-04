using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public List<Inspeccion> GetPaginatedCase(int skip, int take = 5, Expression<Func<Inspeccion, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Inspecciones.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Inspecciones.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
