using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionUsuarioBussines.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionUsuarioBussines.Infrastucture.Repository
{
    public class UsuarioRepository
    {

        private readonly AppDbContext appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }
 
        //traigo orden por estado incluyendo OrderItems
        public async Task<List<Usuario>> GetOrdersByStatus(string nombre)
        {
            return await appDbContext.Usuario.Include(x => x.Domicilio)
                                .Where(x => x.Nombre == nombre).Select(x => new Usuario
                                {
                                    Id = x.Id,
                                    Nombre = x.Nombre,
                                    Mail = x.Mail,
                                    FechaCreacion = x.FechaCreacion,
                                    Domicilio = x.Domicilio
                                }).ToListAsync();
        }

    }
}
