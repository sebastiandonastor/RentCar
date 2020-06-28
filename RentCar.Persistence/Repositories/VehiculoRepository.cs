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
    public class VehiculoRepository : BaseRepository<Vehiculo>, IVehiculoRepository
    {
        public VehiculoRepository(RentCarContext dbContext) : base(dbContext)
        {
        }

        public RentCarContext _context { get { return context; } }

    }
}
