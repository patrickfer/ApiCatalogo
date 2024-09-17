using ApiCatalogo.Context;
using ApiCatalogo.Repositories.Categorias;
using ApiCatalogo.Repositories.Produtos;

namespace ApiCatalogo.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IProdutoRepository _produtoRepo;

        private ICategoriaRepository _categoriaRepo;

        public AppDbContext _context;

        public UnitOfWork (AppDbContext context, IProdutoRepository produtoRepo, ICategoriaRepository categoriaRepo)
        {
            _context = context;
            _produtoRepo = produtoRepo;
            _categoriaRepo = categoriaRepo;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return _produtoRepo = _produtoRepo ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return _categoriaRepo = _categoriaRepo ?? new CategoriaRepository(_context);
            }
        }


        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
