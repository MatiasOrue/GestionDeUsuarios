using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
       
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public DateTime FechaCreacion { get; set; }
        public Domicilio Domicilio { get; set; }
    }
}
