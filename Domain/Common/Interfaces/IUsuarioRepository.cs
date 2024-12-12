using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Common.Interfaces
{
    public interface IUsuarioRepository 
    {
        Task<Usuario> CreateUsuario(Usuario usuario);
        Task<int>UpdateUsuario(Usuario Usuario);
        Task<List<Usuario>> GetUsuarioByValue(string value);
        Task<Usuario> GetUsuarioById(int id);
        Task<int> DeleteUsuario(int id);
    }
}
