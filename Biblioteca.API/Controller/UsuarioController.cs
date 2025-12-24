using Domain.DTO;
using Domain.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Contracts;

namespace Biblioteca.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUSuarioservice _usuarioservice;
        private readonly IAuthService _authService;

        private readonly IValidator<UsuarioRequest> _validator;
        public UsuarioController(IUSuarioservice usuarioservice, IAuthService authService, IValidator<UsuarioRequest> validator)
        {
            _usuarioservice =  usuarioservice;
            _authService = authService;
            _validator = validator;
        }

       

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto login)
        {
            try
            {
                var result = await _authService.Login(login);
                
                
                if (result == null)
                    return Unauthorized("Credenciais inválidas!");

                return Ok(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        [HttpPost("adicionar-usuario")]
        public async Task<IActionResult> AdicionarUsuario(UsuarioRequest model)
        {
            var validator = await _validator.ValidateAsync(model);

            if (!validator.IsValid)
            {
                /*
                  foreach (var erro in validator.Errors)
                   {
                       ModelState.AddModelError(erro.PropertyName, erro.ErrorMessage);
                   }
                 */
               return  BadRequest(validator.Errors);
            }
                
            
            var usuario = await _usuarioservice.AdicionarUsuario(model);
            return Created("", usuario);
        }

        [HttpGet("usuarios")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _usuarioservice.ListarUsuarios();
            return Ok(usuarios);
        }

        [HttpGet("usuario/id")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> BuscarUsuarioId(int id)
        {
            var usuario =  await _usuarioservice.ObterPorId(id);
            return Ok(usuario);
        }

        [HttpPut("editar-usuario")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtualizarUsuario(UsuarioRequest model)
        {
            
            var validator = await _validator.ValidateAsync(model);
            
            if (!validator.IsValid)
                return BadRequest(validator.Errors);
            
            
            
            
            var usuario = await _usuarioservice.AtualizarUsuario(model);
            return Created("", usuario);
        }
    }
}
