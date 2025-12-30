using AutoMapper;
using Domain.DTO;
using Domain.DTO.Response;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class LivroService : ILivroService
{
    
    private readonly ILivroRepository _livroRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUploadPhotoService _uploadPhoto;
    private readonly IMapper _mapper;


    public LivroService(ILivroRepository repository, IUsuarioRepository usuarioRepository, IMapper mapper, IUploadPhotoService uploadPhoto)
    {
        _livroRepository = repository;
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
        _uploadPhoto =  uploadPhoto;
    }
    
    public async Task<List<LivroResponse>> Listar()
    {
        var livros = await _livroRepository.Listar();
        return _mapper.Map<List<LivroResponse>>(livros);
    }

    public async Task<LivroResponse?> GetById(int id)
    {
        try
        {
            var livro = await _livroRepository.GetById(id);
            return _mapper.Map<LivroResponse>(livro);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
        
    }

    public async Task<LivroResponse> Adicionar(LivroRequest livro, int usuarioId, IFormFile? arquivoCapa)
    {
        try
        {
            
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            string urlCapaFinal = livro.CapaUrl;

            if (arquivoCapa != null)
            {
               var uploadResult = await _uploadPhoto.UploadImageAsync(arquivoCapa);
            
                if (uploadResult.Error != null)
                    throw new Exception($"Erro no Cloudinary: {uploadResult.Error.Message}");

                urlCapaFinal = uploadResult.SecureUrl.ToString();
            }
            
            var novoLivro = new Livro
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Isbn = livro.Isbn,
                Editora = livro.Editora,
                NumeroPaginas = livro.NumeroPaginas,
                Categoria = livro.Categoria,
                CapaUrl = urlCapaFinal,
                StatusLeitura = livro.StatusLeitura,
                AnoPublicacao = livro.AnoPublicacao,
                NotasPessoais = livro.NotasPessoais,

                UsuarioId = usuario.Id
            };
            
 

             await _livroRepository.Adicionar(novoLivro);

             return _mapper.Map<LivroResponse>(novoLivro);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.InnerException?.Message);

            throw;
        }
        
    }

    public async Task<LivroResponse> Atualizar(LivroRequest livro)
    {
        try
        {
            var buscarLivro = await _livroRepository.GetById(livro.Id) ?? throw new Exception("Livro não econtrado.");
            
            buscarLivro.Titulo =  livro.Titulo;
            buscarLivro.Autor = livro.Autor;
            buscarLivro.Isbn = livro.Isbn;
            buscarLivro.Editora = livro.Editora;
            buscarLivro.NumeroPaginas = livro.NumeroPaginas;
            buscarLivro.Categoria = livro.Categoria;
            buscarLivro.CapaUrl = livro.CapaUrl;
            buscarLivro.StatusLeitura = livro.StatusLeitura;
            buscarLivro.AnoPublicacao = livro.AnoPublicacao;
            buscarLivro.NotasPessoais = livro.NotasPessoais;
            
            await _livroRepository.Atualizar(buscarLivro);
            
            return _mapper.Map<LivroResponse>(buscarLivro);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message.ToString());
        }
        
        
    }
}