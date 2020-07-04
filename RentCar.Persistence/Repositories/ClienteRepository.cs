using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Generic;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace RentCar.Persistence.Repositories
{
    public class ClienteRepository : BaseRepository<Cliente>, IClienteRepository
    {
        public ClienteRepository(RentCarContext dbContext) : base(dbContext)
        {
        }
        public RentCarContext _context { get { return context; } }

        public void Update(Cliente oldCLiente, Cliente recentCliente)
        {

            oldCLiente.Estado = recentCliente.Estado;
            oldCLiente.IdTipoPersona = recentCliente.IdTipoPersona;
            oldCLiente.LimiteCredito = recentCliente.LimiteCredito;
            oldCLiente.Cedula = recentCliente.Cedula;
            oldCLiente.Nombre = recentCliente.Nombre;
            oldCLiente.NoTarjetaCr = recentCliente.NoTarjetaCr;
        }

        public List<Cliente> GetPaginatedCase(int skip, int take = 5, Expression<Func<Cliente, bool>> predicate = null)
        {
            if (predicate == null)
                return _context.Clientes.OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

            return _context.Clientes.Where(predicate).OrderByDescending(v => v.Id).Skip(skip).Take(take).ToList();

        }
    }
}
