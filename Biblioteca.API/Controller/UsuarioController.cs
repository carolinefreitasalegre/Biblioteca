using Microsoft.AspNetCore.Http;
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

        public UsuarioController(IUSuarioservice usuarioservice)
        {
            _usuarioservice =  usuarioservice;
        }
        
        [Http]
        
    }
}
