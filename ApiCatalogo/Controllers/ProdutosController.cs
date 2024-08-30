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
        private readonly IProdutoRepository _produtoRepository;
        private readonly IRepository<Produto> _repository;

        public ProdutosController(IProdutoRepository produtoRepository, IRepository<Produto> repository)
        {
            _produtoRepository = produtoRepository;
            _repository = repository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetProdutosCategoria(int id)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(id);

            if (produtos is null)
            {
                return NotFound();
            }

            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _repository.GetAll().ToList();

            return Ok(produtos);
        }

        [HttpGet("{id:int}", Name="ObterProduto")]
        public ActionResult Get(int id)
        {
            var produto =_repository.Get(p => p.ProdutoId == id);

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

           var novoProduto = _repository.Create(produto);

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
            var produtoAtualizado = _repository.Update(produto);
                return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
           var produto = _repository.Get(p => p.ProdutoId == id);

            if (produto is null)
            {
                return BadRequest();
            }

            var produtoDeletado = _repository.Delete(produto);

            return Ok(produtoDeletado);
        }
    }
}
