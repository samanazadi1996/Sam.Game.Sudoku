using Moq;
using Shouldly;
using Sudoku.Application.Features.Products.Commands.CreateProduct;
using Sudoku.Application.Interfaces;
using Sudoku.Application.Interfaces.Repositories;

namespace Sudoku.UnitTests.ApplicationTests.Features.Products.Commands
{
    public class CreateProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResultWithProductId()
        {
            // Arrange
            var productRepositoryMock = new Mock<IProductRepository>();
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            var handler = new CreateProductCommandHandler(productRepositoryMock.Object, unitOfWorkMock.Object);

            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 100,
                BarCode = "123456789"
            };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            result.Success.ShouldBeTrue();
        }
    }
}
