using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class VehiculoValidation : AbstractValidator<Vehiculo>
    {
        public VehiculoValidation()
        {
            RuleFor(v => v.Descripcion)
                .Must(d => !String.IsNullOrWhiteSpace(d))
                .WithMessage("Por favof ingresar una descripcion");

            RuleFor(v => v.Chasis)
               .Must(d => !String.IsNullOrWhiteSpace(d))
                            .WithMessage("Por favof ingresar un chasis");


            RuleFor(v => v.NoMotor)
              .Must(d => !String.IsNullOrWhiteSpace(d))
                              .WithMessage("Por favof ingresar una No Motor");


            RuleFor(v => v.Placa)
             .Must(d => !String.IsNullOrWhiteSpace(d))
                             .WithMessage("Por favof ingresar una Placa");


            RuleFor(v => v.IdMarca)
               .GreaterThan(0)
               .WithMessage("Por favor ingresar una marca");

            RuleFor(v => v.IdModelo)
               .GreaterThan(0)
               .WithMessage("Por favor ingresar un modelo");

            RuleFor(v => v.IdTipoCombustible)
               .GreaterThan(0)
               .WithMessage("Por favor ingresar un tipo combustible");

            RuleFor(v => v.IdTipoVehiculo)
              .GreaterThan(0)
              .WithMessage("Por favor ingresar un tipo vehiculo");
        }
    }
}
