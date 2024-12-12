using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.DTO
{
    public class UsuarioDTO
    {

        public int Id { get; set; }
         public string? Nombre { get; set; }
         public string? Mail { get; set; }
         //public DateTime? FechaCreacion { get; set; }
         public DomicilioDTO? Domicilio { get; set; }
    }
}
