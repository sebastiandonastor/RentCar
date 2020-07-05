using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class EmpleadoValidation : AbstractValidator<Empleado>
    {
        public EmpleadoValidation()
        {
            RuleFor(c => c.Nombre).Must(n => !String.IsNullOrWhiteSpace(n))
         .WithMessage("Por favor ingrese un nombre");

            RuleFor(c => c.TandaLabor).Must(n => !String.IsNullOrWhiteSpace(n))
            .WithMessage("Por favor ingrese una tanda laboral");


            RuleFor(c => c.Cedula).
                Must(c => Validations.ValidarCedula(c))
                .WithMessage("Ingrese una cedula dominicana valida");

            RuleFor(c => c.PorcientoComision)
                .GreaterThan(0)
                .WithMessage("Por favor ingresar un limite de credito mayor a 0");

            RuleFor(c => c.FechaIngreso).NotNull()
                .WithMessage("Por favor seleccione una fecha");
        }
    }
}
