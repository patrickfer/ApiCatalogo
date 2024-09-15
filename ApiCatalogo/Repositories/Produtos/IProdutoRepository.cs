using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories.Produtos
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        IEnumerable<Produto> GetProdutos(ProdutosParameters produtosParams)
        {
            return GetAll()
                .OrderBy(p =>p.Nome)
                .Skip((produtosParams.PageNumber - 1) * produtosParams.PageSize)
                .Take(produtosParams.PageSize).ToList();
        }
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
    }
}
