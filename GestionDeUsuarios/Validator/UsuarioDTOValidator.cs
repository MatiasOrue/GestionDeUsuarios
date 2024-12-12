using Domain.DTO;
using Domain.Models;
using FluentValidation;

namespace GestionDeUsuarios.Validator
{
    public class UsuarioDTOValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioDTOValidator()
        {
            RuleFor(usuario => usuario.Nombre)
           .NotEmpty()
           .WithMessage("Se requiere cargar el campo Nombre.");
            RuleFor(usuario => usuario.Mail).NotEmpty()
            .WithMessage("Se requiere cargar el campo Mail.")
            .EmailAddress()
            .WithMessage("Se requiere un mail válido");
            

            RuleFor(usuario => usuario.Domicilio).Custom((domicilio, context) =>
            {
                if (domicilio != null)
                {
                    if (string.IsNullOrEmpty(domicilio.Calle))
                    {
                        context.AddFailure("Domicilio.Calle", "La calle es obligatoria si el domicilio no es nulo.");
                    }
                    if (string.IsNullOrEmpty(domicilio.Numero))
                    {
                        context.AddFailure("Domicilio.Numero", "El número es obligatorio si el domicilio no es nulo.");
                    }
                    if (string.IsNullOrEmpty(domicilio.Provincia))
                    {
                        context.AddFailure("Domicilio.Provincia", "La Provincia es obligatoria si el domicilio no es nulo.");
                    }
                    if (string.IsNullOrEmpty(domicilio.Ciudad))
                    {
                        context.AddFailure("Domicilio.Ciudad", "La Ciudad es obligatorio si el domicilio no es nulo.");
                    }
                }
            });
        }
    }   
}
