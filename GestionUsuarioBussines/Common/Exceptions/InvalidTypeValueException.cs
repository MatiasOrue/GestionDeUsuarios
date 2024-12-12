using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Exceptions
{
    public class InvalidTypeValueException :Exception
    {
        public InvalidTypeValueException() : base("El tipo de valor es inválido") { }
    }
}
