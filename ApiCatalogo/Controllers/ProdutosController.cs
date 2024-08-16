using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos = await _context.Produtos.AsNoTracking().ToListAsync();

            if (produtos is null)
            {
                return NotFound("Produtos não encontrados.");
            }
            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public async Task<ActionResult> Get(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(p=> p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }
            
            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }

            _context.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
