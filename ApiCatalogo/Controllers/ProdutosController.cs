using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using ApiCatalogo.Repositories.Produtos;
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
        public readonly IUnitOfWork _uof;
        public readonly ILogger<ProdutosController> _logger;

        public ProdutosController(IUnitOfWork uof, ILogger<ProdutosController> logger)
        {
            _uof = uof;
            _logger = logger;
        }


        [HttpGet("Categoria/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
            var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _uof.ProdutoRepository.GetAll().ToList();

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult Get(int id)
        {
            var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
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

           var novoProduto = _uof.ProdutoRepository.Create(produto);
            _uof.Commit();

            return new CreatedAtRouteResult("ObterProduto",
                new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }
            var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
            _uof.Commit();

                return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
           var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return BadRequest();
            }

            var produtoDeletado = _uof.ProdutoRepository.Delete(produto);

            return Ok(produtoDeletado);
        }
    }
}
