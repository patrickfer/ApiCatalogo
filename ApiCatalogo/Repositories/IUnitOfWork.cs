using ApiCatalogo.Repositories.Categorias;
using ApiCatalogo.Repositories.Produtos;

namespace ApiCatalogo.Repositories
{
    public interface IUnitOfWork
    {
        IProdutoRepository ProdutoRepository { get; }
        ICategoriaRepository CategoriaRepository { get; }

        Task CommitAsync();
    }
}
