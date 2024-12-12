using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common.Interfaces;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastucture.Services
{
    public class UsuarioService : IUsuarioService
    {

        private readonly IUsuarioRepository usuarioRepository;
        private readonly IMapper _mapper;
        public UsuarioService(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            this.usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }


        public async Task<UsuarioDTO> Create(UsuarioDTO dtoUsuario)
        {
            // Mapear DTO a la entidad Usuario
            var usuarioEntidad = _mapper.Map<Usuario>(dtoUsuario);
            //seteo el Id de usuario para que no me ingrece cualquier Id o repetido
             usuarioEntidad.Id = 0;
            // seteo la fecha actual
             usuarioEntidad.FechaCreacion = DateTime.Now;
            //si domicilio es distinto de null seteo fecha
            if (usuarioEntidad.Domicilio != null)
            {
                
                usuarioEntidad.Domicilio.FechaCreacionDomicilio = DateTime.Now;
            }         
            // Crear nuevo usuario y se devuelve un un objeto dto
            var usuarioCreado = await usuarioRepository.CreateUsuario(usuarioEntidad);
            return _mapper.Map<UsuarioDTO>(usuarioCreado);
            
        }
        //retorno el id del usuario eliminado
        public async Task<int> DeleteUsario(int id)=>
             await usuarioRepository.DeleteUsuario(id);


        //retorna lista de usuario
        public async Task<List<UsuarioDTO>> GetUsuarios(string value)
        {
            var usuarios = await usuarioRepository.GetUsuarioByValue(value) ?? new List<Usuario>();
            return _mapper.Map<List<UsuarioDTO>>(usuarios);
        }


        //retorno el id del usuario modificado
        public async Task<int> UpdateUsario(UsuarioDTO dtoUsuario)
        {
            Usuario user = _mapper.Map<Usuario>(dtoUsuario);
            
            // Obtener el usuario existente
            var usuarioExistente = await usuarioRepository.GetUsuarioById(dtoUsuario.Id);
            if (usuarioExistente == null)
            {
              
                throw new KeyNotFoundException($"El usuario con ID {dtoUsuario.Id} no existe.");
            }
          
            // Mapear desde DTOUsuario a la entidad existente
            _mapper.Map(dtoUsuario, usuarioExistente);
            if(dtoUsuario.Domicilio != null && usuarioExistente.Domicilio.Id == 0)
            {
                usuarioExistente.Domicilio.FechaCreacionDomicilio = DateTime.Now;
            }
            
            // Actualizar el usuario y retorna el id
            return await usuarioRepository.UpdateUsuario(usuarioExistente);
        }

        
    }
}
