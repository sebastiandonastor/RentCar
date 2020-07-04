using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IVehiculoRepository : IBaseRepository<Vehiculo>
    {
        void Update(Vehiculo oldVehiculo, Vehiculo newVehiculo);

        List<Vehiculo> GetPaginatedCase(int skip, int take = 5, Expression<Func<Vehiculo, bool>> predicate = null);

    }
}
