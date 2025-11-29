using Models.Models;
using Repositories.Repositories.Contracts;
using Services.Contracts;

namespace Services.Services;

public class UsuarioService : IUSuarioservice
{
    private readonly IUsuarioRepository _usuarioRepository;

    public UsuarioService(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<List<Usuario>> ListarUsuarios()
    {
        try
        {
            var usuarios = await _usuarioRepository.ListarUsuarios();
            if (!usuarios.Any())
            {
                throw new NullReferenceException("Ainda não há usuário a ser listado.");
            }

            return usuarios;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Usuario?> ObterPorEmail(string email)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterPorEmail(email);
            if (usuario == null)
                throw new Exception("Email não encontrado!");

            return usuario;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Usuario?> ObterPorId(int id)
    {
        try
        {
            var usuario = await _usuarioRepository.ObterPorId(id);
            if (usuario == null)
                throw new Exception("Usuário não encontrado!");

            return usuario;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Usuario> AdicionarUsuario(Usuario usuario, int id)
    {
        try
        {
            var novoUsuario = new Usuario
            {
                Nome = usuario.Nome.ToLower(),
                Email = usuario.Email.ToLower(),
                Senha = usuario.Senha,
                Perfil = usuario.Perfil,
                Status = usuario.Status,
                CriadoEm = usuario.CriadoEm,
            };

            await _usuarioRepository.AdicionarAtualizar(novoUsuario, id: id);
            return novoUsuario;

        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Usuario> AtualizarUsuario(Usuario usuario, int id)
    {
        try
        {
            var usuarioAtualizado = await _usuarioRepository.ObterPorId(id);
            if (usuarioAtualizado == null)
                throw new Exception("Usuário não enconttrado!");

            usuarioAtualizado.Nome = usuario.Nome.ToLower();
            usuarioAtualizado.Email = usuario.Email.ToLower();
            usuarioAtualizado.Senha = usuario.Senha;
            usuarioAtualizado.Perfil = usuario.Perfil;
            usuarioAtualizado.Status = usuario.Status;
            usuarioAtualizado.CriadoEm = usuario.CriadoEm;

            await _usuarioRepository.AdicionarAtualizar(usuarioAtualizado, usuarioAtualizado.Id);
            return usuarioAtualizado;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }

}