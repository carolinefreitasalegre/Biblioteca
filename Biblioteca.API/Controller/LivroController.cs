using System.Security.Claims;
using Domain.DTO;
using Domain.Exceptions;
using Domain.Validator;
using FluentValidation;
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
        // private readonly IValidator<LivroRequest> _validator;
        // private readonly IValidator<LivroRequest> _validatorEditar;

        public LivroController(
            ILivroService livroService)
            // IValidator<LivroRequest> validator, IValidator<LivroRequest> validatorEditar)
        {
            _livroService = livroService;
            // _validator = validator;
            // _validatorEditar = validatorEditar;
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
        public async Task<IActionResult> NewBook(
            [FromForm] LivroRequest model,
            [FromForm(Name = "arquivocapa")] IFormFile capaUrl,
            [FromServices] LivroRequestValidator validator)
        {
            var userIdClaim = User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var usuarioId))
                return Unauthorized("Token inválido ou ID de usuário ausente.");

            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            if (capaUrl == null || capaUrl.Length == 0)
                return BadRequest("A capa do livro é obrigatória.");

            var newBook = await _livroService.Adicionar(model, capaUrl);

            return Created(string.Empty, newBook);
        }


        [HttpPut("editar-livro")]
        public async Task<IActionResult> EditBook(
            [FromBody] LivroRequest model,
            [FromServices] EditarLivroRequestValidator validator)
        {
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var editBook = await _livroService.Atualizar(model);

            return Ok(editBook);
        }

    }

    
}
