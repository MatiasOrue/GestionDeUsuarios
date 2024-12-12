using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common.Interfaces;
using Domain.DTO;
using Domain.Models;
using Infrastucture.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Infrastucture.Persistence.Repository
{
    public class UsuarioRepository :IUsuarioRepository
    {

        private readonly AppDbContext appDbContext;

        public UsuarioRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Usuario> CreateUsuario(Usuario usuario)
        {

            await appDbContext.Usuario.AddAsync(usuario);
            if(usuario.Domicilio != null)
            { //ayudo entityFramework para saber que Domicilio tambien es un alta
                appDbContext.Entry(usuario.Domicilio).State = EntityState.Added;
            }
            
            await appDbContext.SaveChangesAsync();

            // Retornar el usuario creado con su ID generado
            return usuario;
        }

        //traigo el usuario con o sin domicilio segun provincia,ciudad y nombre
        public async Task<List<Usuario>> GetUsuarioByValue(string value)
        {
            return await appDbContext.Usuario
                             .Include(x => x.Domicilio)
                             .Where(x => x.Nombre == value || x.Domicilio.Provincia== value || x.Domicilio.Ciudad== value) 
                             .Select(x => new Usuario
                             {
                                 Id = x.Id,
                                 Nombre = x.Nombre,
                                 Mail = x.Mail,
                                 FechaCreacion = x.FechaCreacion,
                                 Domicilio = x.Domicilio
                             }).ToListAsync();
            
                            
        }
        public async Task<Usuario> GetUsuarioById(int id)
        {
            return await appDbContext.Usuario
                            .Include(x => x.Domicilio)  // Incluye la relación de Domicilio
                            .FirstOrDefaultAsync(x => x.Id == id);  // Busca un único Usuario que coincida con el id
        }


        //modifico usuario
        public async Task<int> UpdateUsuario(Usuario usuario)
        {
            appDbContext.Usuario.Update(usuario);

            if (usuario.Domicilio== null)
            {
                appDbContext.Entry(usuario.Domicilio).State = EntityState.Added;
            }
           else
            {
                appDbContext.Domicilio.Update(usuario.Domicilio);
            }

            await appDbContext.SaveChangesAsync();
            return usuario.Id;
        }

        //elimina usuario por id
        public async Task<int> DeleteUsuario(int id)
        {
            // Busca el usuario existente por ID
            var usuario = await appDbContext.Usuario.FindAsync(id);

            
            if (usuario != null)
            {
                // Remuevo el usuario si existe
                appDbContext.Usuario.Remove(usuario);

             
                await appDbContext.SaveChangesAsync();
                return usuario.Id;
            }
            else
            {
                throw new Exception($"Usuario con ID {id} no encontrado.");
            }
        }

       
    }
}
