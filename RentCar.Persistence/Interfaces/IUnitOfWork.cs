using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> CompleteAsync();
        void Dispose();

        ITipoPersonaRepository TiposPersonas { get; }

        ITipoCombustibileRepository TiposCombustibles { get; }

        ITipoVehiculoRepository TiposVehiculos { get; }

        IClienteRepository Clientes { get; }

        IEmpleadoRepository Empleados { get; }

        IRentaRepository Rentas { get; }

        IInspeccionRepository Inspecciones { get; }

        IMarcaRepository Marcas { get; }

        IModeloRepository Modelos { get; }

        IVehiculoRepository Vehiculos { get; }
    }
}
