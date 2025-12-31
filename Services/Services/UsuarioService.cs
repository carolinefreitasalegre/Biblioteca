using AutoMapper;
using Domain.DTO;
using Domain.DTO.Response;
using Domain.Exceptions;
using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class UsuarioService : IUSuarioservice
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMapper _mapper;

    public UsuarioService(IUsuarioRepository usuarioRepository,  IMapper mapper)
    {
        _usuarioRepository = usuarioRepository;
        _mapper = mapper;
    }

    public async Task<List<UsuarioResponse>> ListarUsuarios()
    {
        try
        {
            var usuarios = await _usuarioRepository.ListarUsuarios();
            if (!usuarios.Any())
            {
                throw new NullReferenceException("Ainda não há usuário a ser listado.");
            }

            return _mapper.Map<List<UsuarioResponse>>(usuarios);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<UsuarioResponse?> ObterPorEmail(string email)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);
            if (usuario == null)
                throw new Exception(ErrorMessages.UsuarioNaoEncontrado);

            return _mapper.Map<UsuarioResponse>(usuario);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<UsuarioResponse?> ObterPorId(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null)
                throw new Exception(ErrorMessages.UsuarioNaoEncontrado);

            return _mapper.Map<UsuarioResponse>(usuario);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<UsuarioResponse> AdicionarUsuario(UsuarioRequest usuario)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome.ToLower(),
                Email = usuario.Email.ToLower(),
                Senha = usuario.Senha,
                Role = usuario.Role,
                Status = usuario.Status,
                CriadoEm = DateTime.UtcNow,
            };

            await _usuarioRepository.Adicionar(novoUsuario);
            return _mapper.Map<UsuarioResponse>(novoUsuario);

        }
        catch (Exception e)
        {
            throw new Exception("", e);
        }
    }

    public async Task<UsuarioResponse> AtualizarUsuario(UsuarioRequest usuario)
    {
        try
        {
            var usuarioAtualizado = await _usuarioRepository.ObterPorId(usuario.Id);
            if (usuarioAtualizado == null)
                throw new Exception("Usuário não enconttrado!");

            usuarioAtualizado.Nome = usuario.Nome.ToLower();
            usuarioAtualizado.Email = usuario.Email.ToLower();
            usuarioAtualizado.Role = usuario.Role;
            usuarioAtualizado.Status = usuario.Status;

            if (!string.IsNullOrWhiteSpace(usuario.Senha))
            {
                usuarioAtualizado.Senha = usuario.Senha;
            }

            await _usuarioRepository.Atualizar(usuarioAtualizado);
            return _mapper.Map<UsuarioResponse>(usuarioAtualizado);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

}