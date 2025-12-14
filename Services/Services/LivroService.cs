using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class LivroService : ILivroService
{
    
    private readonly ILivroRepository _livroRepository;
    private readonly IUsuarioRepository _usuarioRepository;


    public LivroService(ILivroRepository repository, IUsuarioRepository usuarioRepository)
    {
        _livroRepository = repository;
        _usuarioRepository = usuarioRepository;

    }
    
    public async Task<List<Livro>> Listar()
    {
        try
        {
            var livros =  await _livroRepository.Listar();
            return livros;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message.ToString());
        }
       
    }

    public async Task<Livro?> GetById(int id)
    {
        try
        {
            var livro = await _livroRepository.GetById(id);
            return livro;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message.ToString());
        }
        
        
    }

    public async Task<Livro> Adicionar(Livro livro, int usuarioId)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null)
                throw new Exception("Usuário não encontrado.");
            
            var novoLivro = new Livro
            {
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Isbn = livro.Isbn,
                Editora = livro.Editora,
                NumeroPaginas = livro.NumeroPaginas,
                Categoria = livro.Categoria,
                CapaUrl = livro.CapaUrl,
                StatusLeitura = livro.StatusLeitura,
                AnoPublicacao = livro.AnoPublicacao,
                NotasPessoais = livro.NotasPessoais,
                UsuarioId = usuarioId
            };
            return await _livroRepository.Adicionar(novoLivro);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        
    }

    public async Task<Livro> Atualizar(Livro livro)
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
            
            return await _livroRepository.Atualizar(buscarLivro);

        }
        catch (Exception e)
        {
            throw new Exception(e.Message.ToString());
        }
        
        
    }
}