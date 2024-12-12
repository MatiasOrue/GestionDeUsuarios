using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;


namespace Domain.Common.Exceptions
{
    public class UsuarioValidationException : Exception
    {
        public UsuarioValidationException(IEnumerable<ValidationFailure> errors) : base("Errores de validación en UsuarioDTO") { Errors = errors; }
        public IEnumerable<ValidationFailure> Errors { get; }
        public override string ToString()
        {
            return $"{base.ToString()}, Errores: {string.Join(", ", Errors.Select(e => e.ErrorMessage))}";
        }
    }
}
