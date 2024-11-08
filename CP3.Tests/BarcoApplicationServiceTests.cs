﻿using CP3.Application.Services;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using CP3.Domain.Interfaces.Dtos;
using Moq;
using Xunit;

namespace CP3.Tests
{
    public class BarcoApplicationServiceTests
    {
        private readonly Mock<IBarcoRepository> _repositoryMock;
        private readonly BarcoApplicationService _barcoService;

        public BarcoApplicationServiceTests()
        {
            _repositoryMock = new Mock<IBarcoRepository>();
            _barcoService = new BarcoApplicationService(_repositoryMock.Object);
        }

        [Fact]
        public void GetAllBoats_ShouldReturnAllBoats()
        {
            // Arrange
            var barcos = new List<BarcoEntity>
            {
                new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 },
                new BarcoEntity { Id = 2, Nome = "Barco2", Modelo = "Modelo2", Ano = 2005, Tamanho = 12.0 }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(barcos);

            // Act
            var result = _barcoService.GetAllBoats();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal(barcos, result);
        }

        [Fact]
        public void GetBoatById_ShouldReturnBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(barco);

            // Act
            var result = _barcoService.GetBoatById(1);

            // Assert
            Assert.Equal(barco, result);
        }

        [Fact]
        public void AddBoat_ShouldAddBoat()
        {
            // Arrange
            var dtoMock = new Mock<IBarcoDto>();
            dtoMock.SetupGet(d => d.Nome).Returns("Barco1");
            dtoMock.SetupGet(d => d.Modelo).Returns("Modelo1");
            dtoMock.SetupGet(d => d.Ano).Returns(2000);
            dtoMock.SetupGet(d => d.Tamanho).Returns(10.5);
            dtoMock.Setup(d => d.Validate()).Verifiable();

            var barco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _repositoryMock.Setup(r => r.Add(It.IsAny<BarcoEntity>())).Returns(barco);

            // Act
            var result = _barcoService.AddBoat(dtoMock.Object);

            // Assert
            dtoMock.Verify(d => d.Validate(), Times.Once);
            Assert.Equal(barco, result);
        }

        [Fact]
        public void UpdateBoat_ShouldUpdateBoat()
        {
            // Arrange
            var dtoMock = new Mock<IBarcoDto>();
            dtoMock.SetupGet(d => d.Nome).Returns("Barco1Editado");
            dtoMock.SetupGet(d => d.Modelo).Returns("Modelo1Editado");
            dtoMock.SetupGet(d => d.Ano).Returns(2001);
            dtoMock.SetupGet(d => d.Tamanho).Returns(11.0);
            dtoMock.Setup(d => d.Validate()).Verifiable();

            var existingBarco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            var updatedBarco = new BarcoEntity { Id = 1, Nome = "Barco1Editado", Modelo = "Modelo1Editado", Ano = 2001, Tamanho = 11.0 };

            _repositoryMock.Setup(r => r.GetById(1)).Returns(existingBarco);
            _repositoryMock.Setup(r => r.Update(It.IsAny<BarcoEntity>())).Returns(updatedBarco);

            // Act
            var result = _barcoService.UpdateBoat(1, dtoMock.Object);

            // Assert
            dtoMock.Verify(d => d.Validate(), Times.Once);
            Assert.Equal(updatedBarco, result);
        }

        [Fact]
        public void DeleteBoat_ShouldRemoveBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Id = 1, Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _repositoryMock.Setup(r => r.Delete(1)).Returns(barco);

            // Act
            var result = _barcoService.DeleteBoat(1);

            // Assert
            Assert.Equal(barco, result);
        }
    }
}