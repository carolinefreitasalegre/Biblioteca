using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Contracts;

namespace Biblioteca.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {   
            _livroService = livroService;
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
        public async Task<IActionResult> NewBook(Livro model, int usuarioId)
        {
            var newBook = await _livroService.Adicionar(model, usuarioId);

            return Created("", newBook);
        }
        
       
        [HttpPut("editar-livro")]
        public async Task<IActionResult> EditBook(Livro model)
        {
            var editBook = await _livroService.Atualizar(model);
            return Created("", editBook);
        }
    }
}
