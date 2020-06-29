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
    }
}
