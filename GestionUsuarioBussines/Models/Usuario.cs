using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarioBussines.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required] public string Nombre { get; set; }
        [Required] public string Mail { get; set; }
        [Required] public DateTime FechaCreacion { get; set; }
        public Domicilio Domicilio { get; set; }
    }
}
