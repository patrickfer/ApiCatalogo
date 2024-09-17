using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Data;
using X.PagedList;

namespace ApiCatalogo.Repositories.Categorias
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IPagedList<Categoria>> GetCategoriasAsync(CategoriasParameters categoriasParams)
        {
            var categorias = await GetAllAsync();

            var categoriasOrdenadas = categorias.OrderBy(
                                      c => c.CategoriaId)
                                      .AsQueryable();

            //var resultado = PagedList<Categoria>.ToPagedList(categoriasOrdenadas,
            //                                                            categoriasParams.PageNumber, categoriasParams.PageSize);

            var resultado = await categoriasOrdenadas
                                    .ToPagedListAsync(
                                    categoriasParams.PageNumber,
                                    categoriasParams.PageSize);

            return resultado;
        }

        public async Task<IPagedList<Categoria>> GetCategoriasFiltroNomeAsync(CategoriasFiltroNome categoriasFiltroParams)
        {
            var categorias = await GetAllAsync();

            if (!string.IsNullOrEmpty(categoriasFiltroParams.Nome))
            {
                categorias = categorias.Where(
                                        c => c.Nome
                                        .Contains(
                                         categoriasFiltroParams.Nome, 
                                         StringComparison.OrdinalIgnoreCase));
            }

            //var categoriasFiltradas = PagedList<Categoria>.ToPagedList(categorias.AsQueryable(), categoriasFiltroParams.PageNumber,
            //                                                                                              categoriasFiltroParams.PageSize);

            var categoriasFiltradas = await categorias.ToPagedListAsync(
                                                       categoriasFiltroParams.PageNumber,
                                                       categoriasFiltroParams.PageSize);

            return categoriasFiltradas;
        }
    }
}
