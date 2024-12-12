using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.DTO
{
    public class DomicilioDTO
    {
        //public int Id { get; set; }
       
        //public int IdUsuario { get; set; }
        
        public string? Calle { get; set; }
      
        public string? Numero { get; set; }
     
        public string? Provincia { get; set; }
        
        public string? Ciudad { get; set; }
        
        //public DateTime? FechaCreaciónDomicilio { get; set; }

       

    }
}
