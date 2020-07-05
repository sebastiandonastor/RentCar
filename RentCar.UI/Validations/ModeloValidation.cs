using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class ModeloValidation : AbstractValidator<Modelo>
    {
        public ModeloValidation()
        {
            RuleFor(m => m.Descripcion)
                .Must(d => !string.IsNullOrWhiteSpace(d))
                .WithMessage("Por favor ingresar una descripcion");

            RuleFor(m => m.IdMarca)
                .GreaterThan(0)
                .WithMessage("Por favor seleccionar una marca");
        }
    }
}
