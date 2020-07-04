using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RentCar.Persistence.Repositories
{
    public class EmpleadoRepository : BaseRepository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(RentCarContext dbContext) : base(dbContext)
        {
        }
        public RentCarContext _context { get { return context; } }

        public void Update(Empleado oldEmpleado, Empleado recentEmpleado)
        {
            oldEmpleado.Cedula = recentEmpleado.Cedula;
            oldEmpleado.Estado = recentEmpleado.Estado;
            oldEmpleado.PorcientoComision = recentEmpleado.PorcientoComision;
            oldEmpleado.TandaLabor = recentEmpleado.TandaLabor;
            oldEmpleado.Nombre = recentEmpleado.Nombre;
            oldEmpleado.FechaIngreso = recentEmpleado.FechaIngreso;
        }

        public List<Empleado> GetPaginatedCase(int skip, int take = 5, Expression<Func<Empleado, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Empleados.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Empleados.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
