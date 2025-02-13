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
            // arrange par�as� tan�mlamalar� yapt���m�z k�s�m olacak.

            var madiatorMock = new Mock<IMediator>();
            CreateCarCommand createCarCommand = new("Audi", "A4", 150);
            MessageResponse response = new("Araba ba�ar�yla kaydedildi.");
            CancellationToken cancellationToken = new CancellationToken();
            madiatorMock.Setup(x => x.Send(createCarCommand,cancellationToken)).ReturnsAsync(response);

            CarsController carsController = new(madiatorMock.Object);

            // Act
            // act par�as� test edilecek metodu �a��rd���m�z k�s�m olacak.

            var result = carsController.Create(createCarCommand, cancellationToken);

            // Assert
            // assert par�as� ise testin sonucunu kontrol etti�imiz k�s�m olacak.

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<MessageResponse>(okResult.Value);

            Assert.Equal(response.Message, returnValue.Message);
            madiatorMock.Verify(x => x.Send(createCarCommand, cancellationToken), Times.Once);
        }
    }
}