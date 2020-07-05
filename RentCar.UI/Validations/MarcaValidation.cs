using FluentValidation;
using RentCar.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.UI.Validations
{
    public class MarcaValidation : AbstractValidator<Marca>
    {
        public MarcaValidation()
        {
            RuleFor(r => r.Description).Must(d => !String.IsNullOrWhiteSpace(d))
                .WithMessage("Favor llenar la descripcion")
                .OverridePropertyName("Marca");
        }
    }
}
