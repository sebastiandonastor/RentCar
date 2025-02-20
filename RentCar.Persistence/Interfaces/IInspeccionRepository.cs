﻿using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IInspeccionRepository : IBaseRepository<Inspeccion>
    {

        void Update(Inspeccion oldInspeccion, Inspeccion newInspeccion);

        List<Inspeccion> GetPaginatedCase(int skip, int take = 5, Expression<Func<Inspeccion, bool>> predicate = null);

    }
}
