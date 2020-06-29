﻿using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IRentaRepository : IBaseRepository<Renta>
    {
        void Update(Renta oldRenta, Renta recentRenta);
    }
}
