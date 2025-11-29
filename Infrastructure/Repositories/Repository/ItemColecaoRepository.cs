using Models.Models;
using Repositories.Repositories.Contracts;

namespace Repositories.Repositories;

public class ItemColecaoRepository : IItemColecaoRepository
{
    public Task<IList<ItemColecao>> ListarPorUsuario(int usuarioId)
    {
        throw new NotImplementedException();
    }

    public Task<ItemColecao?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ItemColecao?> GetPorLivroEUsuario(int livroId, int usuarioId)
    {
        throw new NotImplementedException();
    }

    public Task<ItemColecao> AdicionarAtualizar(ItemColecao itemColecao, int id)
    {
        throw new NotImplementedException();
    }

    public Task Remover(int id)
    {
        throw new NotImplementedException();
    }
}