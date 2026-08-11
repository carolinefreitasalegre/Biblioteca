using Domain.DTO;
using Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace Biblioteca.API.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService; 
        private readonly IValidator<LivroRequest> _adicionarValidator;

        public LivroController(ILivroService livroService, IValidator<LivroRequest> adicionarValidator)
        {
            _livroService = livroService;
            _adicionarValidator = adicionarValidator;
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
                throw new Exception(ErrorMessages.LivroNaoEncontrado);
            
            return Ok(livro);
        }
      
        
        [HttpPost("adicionar-livro")]
        public async Task<IActionResult> NewBook([FromBody] LivroRequest request)
        {
  
            var valiarResult = await _adicionarValidator.ValidateAsync(request);
            if(!valiarResult.IsValid)
                return BadRequest(valiarResult.Errors);
            
            var newBook = await _livroService.Adicionar(request);

            return Created(string.Empty, newBook);
        }


        [HttpPut("editar-livro")]
        public async Task<IActionResult> EditBook([FromBody] LivroRequest request)
        {
            var validationResult = await _adicionarValidator.ValidateAsync(request);
            
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var editBook = await _livroService.Atualizar(request);
            
            return Ok(editBook);

        }
        

    }

    
}
