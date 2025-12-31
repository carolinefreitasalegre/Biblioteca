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
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
           
        }

        [HttpPost("adicionar-usuario")]
        public async Task<IActionResult> AdicionarUsuario([FromBody] UsuarioRequest model)
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

        
        [HttpGet("buscar-por-email")]
        public async Task<IActionResult> BuscarPorEmail([FromQuery] string email)
        {
            var usuario = await _usuarioservice.ObterPorEmail(email);
            return Ok(usuario);
        }
        
        [HttpPut("editar-usuario")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AtualizarUsuario([FromBody] UsuarioRequest model)
        {
            
            var validatorResult = await _validator.ValidateAsync(model);
            if (!validatorResult.IsValid)
                return BadRequest(validatorResult.Errors);
            
            
            var usuario = await _usuarioservice.AtualizarUsuario(model);

            return Created("", usuario);
        }

    }
}
