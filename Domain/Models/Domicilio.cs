using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Domicilio
    {
        public int Id { get; set; }
    
        public int IdUsuario { get; set; }
        
        public Usuario Usuario { get; set; }
       
        public string Calle { get; set; }
      
        public string Numero { get; set; }
        
        public string Provincia { get; set; }
       
        public string Ciudad { get; set; }
   
        public DateTime FechaCreacionDomicilio { get; set; }
    }
}
