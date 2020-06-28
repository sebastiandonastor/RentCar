using RentCar.DAL.SQL;
using RentCar.Entities.Models;
using RentCar.Persistence.Interfaces;
using RentCar.Persistence.Repositories;
using System;
using System.Threading.Tasks;

namespace Persistence.Generic
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly RentCarContext _dbContext;

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public UnitOfWork(RentCarContext dbContext)
        {
            _dbContext = dbContext;
            TiposPersonas = new TipoPersonaRepository(_dbContext);
            TiposCombustibles = new TipoCombustibleRepository(_dbContext);
            TiposVehiculos = new TipoVehiculoRepository(_dbContext);
            Rentas = new RentaRepository(_dbContext);
            Inspecciones = new InspeccionRepository(_dbContext);
            Marcas = new MarcaRepository(_dbContext);
            Modelos = new ModeloRepository(_dbContext);
            Vehiculos = new VehiculoRepository(_dbContext);
            Clientes = new ClienteRepository(_dbContext);
            Empleados = new EmpleadoRepository(_dbContext);
        }

        public ITipoPersonaRepository TiposPersonas { get; private set; }

        public ITipoCombustibileRepository TiposCombustibles { get; private set; }

        public ITipoVehiculoRepository TiposVehiculos { get; private set; }

        public IClienteRepository Clientes { get; private set; }

        public IEmpleadoRepository Empleados { get; private set; }

        public IRentaRepository Rentas { get; private set; }

        public IInspeccionRepository Inspecciones { get; private set; }

        public IMarcaRepository Marcas { get; private set; }

        public IModeloRepository Modelos { get; private set; }

        public IVehiculoRepository Vehiculos { get; private set; }
    }
}