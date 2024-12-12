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
    public interface IUsuarioService
    {
        Task<List<UsuarioDTO>> GetUsuarios(string value);
        Task<int> UpdateUsario(UsuarioDTO usuario);

        Task<int> DeleteUsario(int id);

        Task<UsuarioDTO> Create(UsuarioDTO usuario);
    }
}
