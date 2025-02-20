﻿using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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

        public void Update(Vehiculo oldVehiculo, Vehiculo newVehiculo)
        {
            oldVehiculo.IdTipoVehiculo = newVehiculo.IdTipoVehiculo;
            oldVehiculo.IdTipoCombustible = newVehiculo.IdTipoCombustible;
            oldVehiculo.IdModelo = newVehiculo.IdModelo;
            oldVehiculo.IdMarca = newVehiculo.IdMarca;
            oldVehiculo.NoMotor = newVehiculo.NoMotor;
            oldVehiculo.Placa = newVehiculo.Placa;
            oldVehiculo.Chasis = newVehiculo.Chasis;
            oldVehiculo.Descripcion = newVehiculo.Descripcion;
            oldVehiculo.Estado = newVehiculo.Estado;

        }

        public List<Vehiculo> GetPaginatedCase(int skip, int take = 5, Expression<Func<Vehiculo, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Vehiculos.Include(v => v.Modelo).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();


            return _context.Vehiculos.Include(v => v.Modelo).Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
