using ApiCatalogo.Controllers;
using ApiCatalogo.DTOs;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTests.UnitTests
{
    public class PutProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public PutProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }

        //testes de unidade para PUT
        [Fact]
        public async Task PutProduto_Return_OkResult() 
        {
            //Arrange
            var prodId = 2;

            var updatedProdutoDto = new ProdutoDTO 
            { 
                ProdutoId = prodId,
                Nome = "ProdutoAtualizado - Testes",
                Descricao = "Minha Descricao",
                ImagemUrl = "imagem,jpg",
                CategoriaId = 2
            };

            //Act
            var result = await _controller.Put(prodId, updatedProdutoDto) as ActionResult<ProdutoDTO>;

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();  
        }

        [Fact]
        public async Task PutProduto_Return_BadRequest()
        {
            //Arrange
            var prodId = 1000;

            var updatedProdutoDto = new ProdutoDTO
            {
                ProdutoId = 2,
                Nome = "ProdutoAtualizado - Testes",
                Descricao = "Minha Descricao",
                ImagemUrl = "imagem,jpg",
                CategoriaId = 2
            };

            //Act
            var result = await _controller.Put(prodId, updatedProdutoDto);

            //Assert
            result.Result.Should().BeOfType<BadRequestResult>().Which.StatusCode.Should().Be(400);
        }
    }
}
