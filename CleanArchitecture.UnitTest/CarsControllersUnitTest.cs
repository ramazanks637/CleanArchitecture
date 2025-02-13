using CleanArchitecture.Application.Feature.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CleanArchitecture.UnitTest
{
    public class CarsControllersUnitTest
    {
        [Fact]
        public void Create_ReturnsOKResult_WhenRequestIsValid()
        {
            // Arrange  
            // arrange parçasý tanýmlamalarý yaptýðýmýz kýsým olacak.

            var madiatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new("Audi", "A4", 150);
            MessageResponse response = new("Araba baþarýyla kaydedildi.");
            CancellationToken cancellationToken = new CancellationToken();
            madiatorMock.Setup(x => x.Send(createCarCommand,cancellationToken)).ReturnsAsync(response);

            CarsController carsController = new(madiatorMock.Object);

            // Act
            // act parçasý test edilecek metodu çaðýrdýðýmýz kýsým olacak.

            var result = carsController.Create(createCarCommand, cancellationToken);

            // Assert
            // assert parçasý ise testin sonucunu kontrol ettiðimiz kýsým olacak.

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(response.Message, returnValue.Message);
            madiatorMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
        }
    }
}