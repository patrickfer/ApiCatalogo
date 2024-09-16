
using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;

namespace ApiCatalogo.Repositories.Categorias
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams);
        PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriaParams);

    }
}
