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
    public class GetProdutoUnitTests : IClassFixture<ProdutosUnitTestController>
    {
        private ProdutosController _controller;

        public GetProdutoUnitTests(ProdutosUnitTestController controller)
        {
            _controller = new ProdutosController(controller.repository, controller.mapper);
        }

        [Fact]
        public async Task GetProdutoById_OkResult()
        {
            //Arrange
            var prodId = 2;

            //Act
            var data = await _controller.Get(prodId);

            //Assert (xunit)
           // var okResult = Assert.IsType<OkObjectResult>(data.Result);
            //Assert.Equal(200, okResult.StatusCode);

            //Assert (fluentassertions)
            data.Result.Should().BeOfType<OkObjectResult>() //verifica se o resultado é do tipo OkObjectResult.
                       .Which.StatusCode.Should().Be(200); //verifica se o código de status do OKObjectResult é 200.
        }

        [Fact]
        public async Task GetProdutoById_BadRequest()
        {
            //Arrange
            var prodId = -1;

            //Act
            var data = await _controller.Get(prodId);

            //Assert (xunit)
             //var badRequestResult = Assert.IsType<BadRequestObjectResult>(data.Result);
            //Assert.Equal(400, badRequestResult.StatusCode);

            //Assert (fluentassertions)
            data.Result.Should().BeOfType<BadRequestObjectResult>() //verifica se o resultado é do tipo OkObjectResult.
                .Which.StatusCode.Should().Be(400); //verifica se o código de status do OKObjectResult é 200.
        }

        [Fact]
        public async Task GetProdutoById_NotFound()
        {
            //Arrange
            var prodId = 999;

            //Act
            var data = await _controller.Get(prodId);

            //Assert (xunit)
            //var badRequestResult = Assert.IsType<BadRequestObjectResult>(data.Result);
            //Assert.Equal(400, badRequestResult.StatusCode);

            //Assert (fluentassertions)
            data.Result.Should().BeOfType<NotFoundObjectResult>() //verifica se o resultado é do tipo OkObjectResult.
                .Which.StatusCode.Should().Be(404); //verifica se o código de status do OKObjectResult é 200.
        }

        [Fact]
        public async Task GetProdutos_Return_ListOfProdutoDTO()
        {

            //Act
            var data = await _controller.Get();

            //Assert (fluentassertions)
            data.Result.Should().BeOfType<OkObjectResult>() //verifica se o resultado é do tipo OkObjectResult.
                .Which.Value.Should().BeAssignableTo<IEnumerable<ProdutoDTO>>() //Verifica se o valor do OkObjectResult é atríbuivel a IEnumerable<ProdutoDTO>.
                .And.NotBeNull();
        }

        [Fact]
        public async Task GetProdutos_Return_BadRequestResult()
        {

            //Act
            var data = await _controller.Get();

            //Assert (fluentassertions)
            data.Result.Should().BeOfType<BadRequestResult>()
                .Which.StatusCode.Should().Be(400);
        }
    }
}
