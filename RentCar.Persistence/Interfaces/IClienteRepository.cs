﻿using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IClienteRepository : IBaseRepository<Cliente>
    {
        void Update(Cliente oldCLiente, Cliente recentCliente);
    }
}
