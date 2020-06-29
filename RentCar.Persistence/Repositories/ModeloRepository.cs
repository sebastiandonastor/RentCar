using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
    }
}
