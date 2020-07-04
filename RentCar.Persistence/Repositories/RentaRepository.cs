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
    public class RentaRepository : BaseRepository<Renta>, IRentaRepository
    {
        public RentaRepository(RentCarContext dbContext) : base(dbContext)
        {
        }

        public RentCarContext _context { get { return context; } }

        public void Update(Renta oldRenta, Renta recentRenta)
        {
            oldRenta.Estado = recentRenta.Estado;
            oldRenta.CantidadDias = recentRenta.CantidadDias;
            oldRenta.IdCliente = recentRenta.IdCliente;
            oldRenta.IdEmpleado = recentRenta.IdEmpleado;
            oldRenta.FechaRenta = recentRenta.FechaRenta;
            oldRenta.FechaDevolucion = recentRenta.FechaDevolucion;
            oldRenta.IdVehiculo = recentRenta.IdVehiculo;
            oldRenta.Comentario = recentRenta.Comentario;
        }


        public List<Renta> GetPaginatedCase(int skip, int take = 5, Expression<Func<Renta, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Rentas.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Rentas.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
