using Domain.DTO;
using Domain.Validator;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public UsuarioController(IUSuarioservice usuarioservice, IAuthService authService)
        {
            _usuarioservice =  usuarioservice;
            _authService = authService;
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
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioRequest model, 
            [FromServices] UsuariorequestValidator validator)
        {
            var validatorResult = await _validator.ValidateAsync(model);

            if (!validatorResult.IsValid)
            {
                 
               return  BadRequest(validatorResult.Errors);
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
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioRequest model, 
            [FromServices] UsuarioUpdateValidator validator)
        {
            
            var validatorResult = await validator.ValidateAsync(model);

            Console.WriteLine(validatorResult);
            
            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);
            
            
            var usuario = await _usuarioservice.AtualizarUsuario(model);

            return Created("", usuario);
        }
    }
}
