using CP3.Data.AppData;
using CP3.Data.Repositories;
using CP3.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CP3.Tests
{
    public class BarcoRepositoryTests
    {
        private readonly DbContextOptions<ApplicationContext> _options;
        private readonly ApplicationContext _context;
        private readonly BarcoRepository _barcoRepository;

        public BarcoRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "BarcoTestDb")
                .Options;

            _context = new ApplicationContext(_options);
            _barcoRepository = new BarcoRepository(_context);
        }

        [Fact]
        public void GetAll_ShouldReturnAllBoats()
        {
            // Arrange
            var barco1 = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            var barco2 = new BarcoEntity { Nome = "Barco2", Modelo = "Modelo2", Ano = 2005, Tamanho = 12.0 };

            _context.Barcos.AddRange(barco1, barco2);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.GetAll();

            // Assert
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public void GetById_ShouldReturnBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.GetById(barco.Id);

            // Assert
            Assert.Equal(barco, result);
        }

        [Fact]
        public void Add_ShouldAddBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };

            // Act
            var result = _barcoRepository.Add(barco);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, _context.Barcos.Count());
        }

        [Fact]
        public void Update_ShouldUpdateBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            barco.Nome = "Barco1Editado";

            // Act
            var result = _barcoRepository.Update(barco);

            // Assert
            var updatedBarco = _context.Barcos.Find(barco.Id);
            Assert.Equal("Barco1Editado", updatedBarco.Nome);
        }

        [Fact]
        public void Delete_ShouldRemoveBoat()
        {
            // Arrange
            var barco = new BarcoEntity { Nome = "Barco1", Modelo = "Modelo1", Ano = 2000, Tamanho = 10.5 };
            _context.Barcos.Add(barco);
            _context.SaveChanges();

            // Act
            var result = _barcoRepository.Delete(barco.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(0, _context.Barcos.Count());
        }
    }
}
