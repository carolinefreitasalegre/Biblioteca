using Models.Models;

namespace Services.Contracts;

public interface IItemColecaoService
{
    Task<IList<ItemColecao>> ListarPorUsuario(int usuarioId);
    Task<ItemColecao?> GetById(int id);
    Task<ItemColecao?> GetPorLivroEUsuario(int livroId, int usuarioId);
    Task<ItemColecao> AdicionarAtualizar(ItemColecao itemColecao, int id);
    Task Remover(int id);
}