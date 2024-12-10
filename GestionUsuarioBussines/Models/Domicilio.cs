using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionUsuarioBussines.Models
{
    public class Domicilio
    {
        public int Id { get; set; }
        [Required] 
        public int IdUsuario { get; set; }
        [Required]
        public Usuario Usuario { get; set; }
        [Required] 
        public string Calle { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Provincia { get; set; }
        [Required]
        public string Ciudad { get; set; }
        [Required] 
        public DateTime FechaCreaciónDomicilio { get; set; }
    }
}
