using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class RentaValidation : AbstractValidator<Renta>
    {
        public RentaValidation()
        {
            RuleFor(r => r.IdEmpleado)
                .GreaterThan(0)
                .WithMessage("Por favor seleccionar un empleado");

            RuleFor(r => r.IdVehiculo)
               .GreaterThan(0)
               .WithMessage("Por favor seleccionar un vehiculo");

            RuleFor(r => r.IdCliente)
               .GreaterThan(0)
               .WithMessage("Por favor seleccionar un cliente");

            RuleFor(r => r.MontoDiario)
               .GreaterThan(0)
               .WithMessage("Por favor digite un Monto diario mayor a 0");


            RuleFor(r => r.Comentario)
               .Must(c => !string.IsNullOrWhiteSpace(c))
               .WithMessage("Por favor ingrese un comentario");


        }
    }
}
