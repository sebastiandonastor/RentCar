using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IModeloRepository : IBaseRepository<Modelo>
    {
        void Update(Modelo oldModelo, Modelo recentModelo);
        List<Modelo> GetModelosWithMarcas();

        List<Modelo> GetPaginatedCase(int skip, int take = 5, Expression<Func<Modelo, bool>> predicate = null);

    }
}
