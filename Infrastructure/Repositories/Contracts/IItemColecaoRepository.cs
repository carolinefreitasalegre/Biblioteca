using Models.Models;

namespace Repositories.Repositories.Contracts;

public interface IItemColecaoRepository
{
    Task<IList<ItemColecao>> ListarPorUsuario(int usuarioId);
    Task<ItemColecao?> GetById(int id);
    Task<ItemColecao?> GetPorLivroEUsuario(int livroId, int usuarioId);
    Task<ItemColecao> AdicionarAtualizar(ItemColecao itemColecao, int id);
    Task Remover(int id);
}