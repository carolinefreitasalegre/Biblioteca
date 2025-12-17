using System.Security.Claims;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Contracts;

namespace Biblioteca.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {   
            _livroService = livroService;
        }

        private int? ObterUsuarioLogado()
        {
            var claim = HttpContext.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            
            if (claim == null)
                return null;
            
            return int.TryParse(claim.Value, out var id) ? id : null;
        }
        
        [HttpGet("livros")]
        public async Task<IActionResult> GetLivros()
        {
            var livros = await _livroService.Listar();
            return Ok(livros);
        }

        [HttpGet("livro/{id}")]
        public async Task<IActionResult> GetLivroById(int id)
        {
            var livro = await _livroService.GetById(id);
            
            if (livro == null)
                return NotFound("Livro nao encontrado.");
            
            return Ok(livro);
        }
        
        [HttpPost("adicionar-livro")]
        public async Task<IActionResult> NewBook(LivroRequest model)
        {
            
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            
            if(string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var usuarioId))
                return Unauthorized("Token inválido ou ID de usuário ausente/malformado.");


            Console.WriteLine(userIdClaim);
            Console.WriteLine(User.Identity?.IsAuthenticated);

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }

            var newBook = await _livroService.Adicionar(model, usuarioId);

            return Created("", newBook);
        }

        [HttpPut("editar-livro")]
        public async Task<IActionResult> EditBook(LivroRequest model)
        {
            var editBook = await _livroService.Atualizar(model);
            return Created("", editBook);
        }
    }
}
