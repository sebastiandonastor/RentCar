using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nombre).Must(n => !String.IsNullOrWhiteSpace(n))
                .WithMessage("Por favor ingrese un nombre");

            RuleFor(c => c.Cedula).
                Must(c => Validations.ValidarCedula(c))
                .WithMessage("Ingrese una cedula dominicana valida");

            RuleFor(c => c.LimiteCredito)
                .GreaterThan(0)
                .WithMessage("Por favor ingresar un limite de credito mayor a 0");

            RuleFor(c => c.IdTipoPersona)
                .GreaterThan(0)
                .WithMessage("Por favor elegir un tipo de persona");
        }
    }
}
