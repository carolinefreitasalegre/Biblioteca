using Domain.DTO;
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
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> AdicionarUsuario(Usuario model)
        {
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
        public async Task<IActionResult> AtualizarUsuario(Usuario model)
        {
            var usuario = await _usuarioservice.AtualizarUsuario(model);
            return Created("", usuario);
        }
    }
}
