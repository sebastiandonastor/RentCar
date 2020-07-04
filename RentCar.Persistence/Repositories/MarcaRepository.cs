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
    public class MarcaRepository : BaseRepository<Marca>, IMarcaRepository
    {
        public MarcaRepository(RentCarContext dbContext) : base(dbContext)
        {
        }

        public RentCarContext _context { get { return context; } }

        public void Updat(Marca oldMarca, Marca recentMarca)
        {
            oldMarca.Estado = recentMarca.Estado;
            oldMarca.Description = recentMarca.Description;
        }

        public List<Marca> GetPaginatedCase(int skip, int take = 5, Expression<Func<Marca, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Marcas.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Marcas.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
