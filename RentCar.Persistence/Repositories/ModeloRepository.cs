using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace RentCar.Persistence.Repositories
{
    public class ModeloRepository : BaseRepository<Modelo>, IModeloRepository
    {
        public ModeloRepository(RentCarContext dbContext) : base(dbContext)
        {
        }

        public RentCarContext _context { get { return context; } }

        public List<Modelo> GetModelosWithMarcas()
        {
            return _context.Modelos.Include(m => m.Marca).ToList();
        }

        public void Update(Modelo oldModelo, Modelo recentModelo)
        {
            oldModelo.IdMarca = recentModelo.IdMarca;
            oldModelo.Descripcion = recentModelo.Descripcion;
            oldModelo.Estado = recentModelo.Estado;
        }

        public List<Modelo> GetPaginatedCase(int skip, int take = 5, Expression<Func<Modelo, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Modelos.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Modelos.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
