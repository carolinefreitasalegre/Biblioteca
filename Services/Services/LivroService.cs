using AutoMapper;
using Domain.DTO;
using Domain.DTO.Response;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class LivroService : ILivroService
{
    private readonly ILoggedinUser _loggedinUser;
    private readonly ILivroRepository _livroRepository;
    private readonly IUploadPhotoService _uploadPhoto;
    private readonly IMapper _mapper;

    public LivroService(
        ILivroRepository livroRepository,
        IMapper mapper,
        IUploadPhotoService uploadPhoto,
        ILoggedinUser loggedinUser)
    {
        _livroRepository = livroRepository;
        _mapper = mapper;
        _uploadPhoto = uploadPhoto;
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

    public async Task<LivroResponse> Adicionar(LivroRequest livro, IFormFile? arquivoCapa)
    {
        string capaUrl = livro.CapaUrl;

        if (arquivoCapa != null)
        {
            var uploadResult = await _uploadPhoto.UploadImageAsync(arquivoCapa);

            if (uploadResult.Error != null)
                throw new Exception(uploadResult.Error.Message);

            capaUrl = uploadResult.SecureUrl.ToString();
        }

        var novoLivro = _mapper.Map<Livro>(livro);
        novoLivro.UsuarioId = _loggedinUser.UserId;
        novoLivro.CapaUrl = capaUrl;

        await _livroRepository.Adicionar(novoLivro);

        return _mapper.Map<LivroResponse>(novoLivro);
    }

    public async Task<LivroResponse> Atualizar(LivroRequest livro)
    {
        var livroDb = await _livroRepository.GetById(livro.Id)
            ?? throw new Exception(ErrorMessages.LivroNaoEncontrado);

        if (livroDb.UsuarioId != _loggedinUser.UserId)
            throw new UnauthorizedAccessException();

        _mapper.Map(livro, livroDb);

        await _livroRepository.Atualizar(livroDb);

        return _mapper.Map<LivroResponse>(livroDb);
    }
}
