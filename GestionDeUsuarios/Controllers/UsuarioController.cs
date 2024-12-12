using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Domain.Common.Exceptions;
using Domain.Common.Interfaces;
using Domain.DTO;
using Domain.Models;
using FluentValidation;
using Infrastucture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionDeUsuarios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IValidator<UsuarioDTO> _validator;
        public UsuarioController(IValidator<UsuarioDTO> validator,IUsuarioService usuarioService)
        {
            this._usuarioService= usuarioService;
            this._validator= validator;
        }


        [HttpPost]
        public async Task<ActionResult<UsuarioDTO>> Create([FromBody] UsuarioDTO usuario)
        {
            try
            {
                var validationResult = _validator.Validate(usuario);
                if (!validationResult.IsValid)
                {
                    //clase exception personalizada
                    throw new UsuarioValidationException(validationResult.Errors);
                }
                var objUsuario = await _usuarioService.Create(usuario); 
                return Ok($"El usuario con id {objUsuario.Id} se cargó exitosamente.");
            }
            
            catch (UsuarioValidationException ex)
            {
                foreach (var error in ex.Errors)
                {

                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);

                } 
                
                return BadRequest(ModelState);
                
            }
            catch (Exception ex)
            {
                    ModelState.AddModelError(string.Empty, $"Ha ocurrido un error: {ex.Message}");
                    return StatusCode(500, ModelState);  
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioDTO>>> GetUsuarios(string value)
        {
            try
            {
                if (int.TryParse(value, out _))
                {
                    throw new InvalidTypeValueException();
                }
                return await _usuarioService.GetUsuarios(value);
            }
            catch (InvalidTypeValueException ex)
            {
                ModelState.AddModelError("value", ex.Message);
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error: {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }


        [HttpPut]
        //retorno el id modificado
        public async Task<ActionResult<int>> Edit(UsuarioDTO usuario)
        {
            try
            {
                var validationResult = _validator.Validate(usuario);
                if (!validationResult.IsValid)
                {
                    throw new UsuarioValidationException(validationResult.Errors);
                }

                var id = await _usuarioService.UpdateUsario(usuario);
                return Ok($"El usuario con id {id} se modificó exitosamente.");

            }
            catch (UsuarioValidationException ex)
            {
                foreach (var error in ex.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }

                return BadRequest(ModelState);
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Ha ocurrido un error: {ex.Message}");
                return StatusCode(500, ModelState);
            }
        }

        [HttpDelete("{id}")]
        //retorno el id modificado
        public async Task<ActionResult<int>> DeleteUsuario(int id)
        {
            try
            {
                var usuarioId = await _usuarioService.DeleteUsario(id);
                if (usuarioId >0) 
                { 
                    return Ok($"El usuario con id {id} se eliminó exitosamente."); 
                } 
                else 
                { 
                    return NotFound($"El usuario con id {id} no se encontró."); 
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ha ocurrido un error: {ex.Message}");
            }
        }















    }
}
