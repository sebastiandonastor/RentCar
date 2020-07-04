﻿using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface ITipoVehiculoRepository : IBaseRepository<TipoVehiculo>
    {
        void Update(TipoVehiculo oldTipoVehiculo, TipoVehiculo newTipoVehiculo);

    }
}
