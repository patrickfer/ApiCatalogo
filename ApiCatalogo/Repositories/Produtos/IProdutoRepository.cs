using ApiCatalogo.Models;

namespace ApiCatalogo.Repositories.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
    }
}
