using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IMarcaRepository : IBaseRepository<Marca>
    {
        void Updat(Marca oldMarca, Marca recentMarca);

        List<Marca> GetPaginatedCase(int skip, int take = 5, Expression<Func<Marca, bool>> predicate = null);

    }
}
