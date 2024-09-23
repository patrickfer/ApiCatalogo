using ApiCatalogo.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoXUnitTests.UnitTests
{
    public class DeleteProdutoUnitTests: IClassFixture<ProdutosUnitTestController>
    {
        private readonly ProdutosController _controller;

        public DeleteProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }

        //testes de unidade para DELETE
        [Fact]
        public async Task DeleteProduto_Return_OkResult()
        {
            //Arrange
            var prodId = 2;

            //Act
            var result = await _controller.Delete(prodId);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<OkObjectResult>();

        }

        [Fact]
        public async Task DeleteProduto_Return_NotFound()
        {
            //Arrange
            var prodId = 999;

            //Act
            var result = await _controller.Delete(prodId);

            //Assert
            result.Should().NotBeNull();
            result.Result.Should().BeOfType<NotFoundObjectResult>().Which.StatusCode.Should().Be(404);
        }
    }
}
