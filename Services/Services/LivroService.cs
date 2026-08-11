using AutoMapper;
using Domain.DTO;
using Domain.DTO.Response;
using Domain.Exceptions;
using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class LivroService : ILivroService
{
    private readonly ILoggedinUser _loggedinUser;
    private readonly ILivroRepository _livroRepository;
    private readonly IUploadPhotoService _uploadImageCapa;
    private readonly IMapper _mapper;

    public LivroService(
        ILivroRepository livroRepository,
        IMapper mapper,
        IUploadPhotoService uploadImageCapa,
        ILoggedinUser loggedinUser)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
        _uploadImageCapa = uploadImageCapa;
        _loggedinUser = loggedinUser;
    }

    public async Task<List<LivroResponse>> Listar()
    {
        var userId = _loggedinUser.UserId;

        var livros = await _livroRepository.Listar(userId);

        return _mapper.Map<List<LivroResponse>>(livros);
    }

    public async Task<LivroResponse?> GetById(int id)
    {
        var livro = await _livroRepository.GetById(id);

        if (livro == null)
            return null;

        if (livro.UsuarioId != _loggedinUser.UserId)
            throw new UnauthorizedAccessException();

        return _mapper.Map<LivroResponse>(livro);
    }



    public async Task<LivroResponse> Adicionar(LivroRequest request)
    {
        var novoLivro = _mapper.Map<Livro>(request);
        novoLivro.UsuarioId = _loggedinUser.UserId;

        if (!string.IsNullOrWhiteSpace(request.CapaBase64))
        {
            var uploadResult = await _uploadImageCapa.UploadImageAsync(request.CapaBase64);

            if (uploadResult == null)
                throw new Exception("Falha ao enviar a imagem da capa: resultado nulo.");

            if (uploadResult.Error != null)
                throw new Exception($"Falha ao enviar a imagem da capa: {uploadResult.Error.Message}");

            novoLivro.CapaUrl = uploadResult.SecureUrl.ToString();
        }
        
        await _livroRepository.Adicionar(novoLivro);

        return _mapper.Map<LivroResponse>(novoLivro);
    }
    
    public async Task<LivroResponse> Atualizar(LivroRequest request)
    {
        var livroExistente = await _livroRepository.GetById(request.Id);

        if (livroExistente == null)
            throw new Exception(ErrorMessages.LivroNaoEncontrado);

        if (livroExistente.UsuarioId != _loggedinUser.UserId)
            throw new UnauthorizedAccessException("Você não tem permissão para editar este livro.");

        _mapper.Map(request, livroExistente);

        if (!string.IsNullOrWhiteSpace(request.CapaBase64))
        {
            var uploadResult = await _uploadImageCapa.UploadImageAsync(request.CapaBase64);

            if (uploadResult == null)
                throw new Exception("Falha ao enviar a imagem da capa: resultado nulo.");

            if (uploadResult.Error != null)
                throw new Exception($"Falha ao enviar a imagem da capa: {uploadResult.Error.Message}");

            livroExistente.CapaUrl = uploadResult.SecureUrl.ToString();
        }

        await _livroRepository.Atualizar(livroExistente);

        return _mapper.Map<LivroResponse>(livroExistente);
    }
    
}
