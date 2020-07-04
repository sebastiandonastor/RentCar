using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IEmpleadoRepository : IBaseRepository<Empleado>
    {
        void Update(Empleado oldEmpleado, Empleado recentEmpleado);

        List<Empleado> GetPaginatedCase(int skip, int take = 5, Expression<Func<Empleado, bool>> predicate = null);

    }
}
