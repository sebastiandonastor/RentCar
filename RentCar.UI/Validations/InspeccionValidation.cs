using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class InspeccionValidation : AbstractValidator<Inspeccion>
    {
        public InspeccionValidation()
        {
            RuleFor(i => i.IdVehiculo)
                .GreaterThan(0)
                .WithMessage("Por favor seleccione un vehiculo");

            RuleFor(i => i.IdCliente)
                .GreaterThan(0)
                .WithMessage("Por favor seleccione un empleado");

            RuleFor(i => i.IdCliente)
                .GreaterThan(0)
                .WithMessage("Por favor seleccione un cliente");

            RuleFor(i => i.CantidadCombustible)
                .Must(c => !string.IsNullOrWhiteSpace(c))
                .WithMessage("Por favor seleccione una cantidad de combustible");
        }
    }
}
