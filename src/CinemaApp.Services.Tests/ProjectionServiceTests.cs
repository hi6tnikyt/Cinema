using Moq;
using NUnit.Framework;
using CinemaApp.Data.Models;
using CinemaApp.Data.Repository.Contracts;
using CinemaApp.Services.Core;
using System.Linq.Expressions;

namespace CinemaApp.Services.Tests
{
    [TestFixture]
    public class ProjectionServiceTests
    {
        private Mock<IProjectionRepository> _mockRepository;
        private ProjectionService _projectionService;

        [SetUp]
        public void SetUp()
        {
            // 1. Създаваме мок на репозиторито
            _mockRepository = new Mock<IProjectionRepository>();

            // 2. Инжектираме мок-а в сървиса
            _projectionService = new ProjectionService(_mockRepository.Object);
        }

        [Test]
        public async Task GetProjectionIdByMovieCinemaAndShowtimeAsync_ShouldReturnId_WhenProjectionExists()
        {
            // Arrange (Подготовка)
            Guid movieId = Guid.NewGuid();
            Guid cinemaId = Guid.NewGuid();
            DateTime showtime = new DateTime(2026, 3, 30, 18, 30, 0); // 18:30ч.
            Guid projectionId = Guid.NewGuid();
            TimeOnly timeOnly = TimeOnly.FromDateTime(showtime);

            List<Projection> projections = new List<Projection>
            {
                new Projection
                {
                    Id = projectionId,
                    MovieId = movieId,
                    CinemaId = cinemaId,
                    Showtime = timeOnly
                }
            };

            // Настройваме мок-а да връща нашия списък при извикване на GetProjectionsAsync
            _mockRepository
                .Setup(repo => repo.GetProjectionsAsync(It.IsAny<Expression<Func<Projection, bool>>>()))
                .ReturnsAsync(projections);

            // Act (Действие)
            var result = await _projectionService.GetProjectionIdByMovieCinemaAndShowtimeAsync(movieId, cinemaId, showtime);

            // Assert (Проверка)
            Assert.That(result, Is.EqualTo(projectionId));
        }

        [Test]
        public async Task GetProjectionIdByMovieCinemaAndShowtimeAsync_ShouldReturnNull_WhenProjectionDoesNotExist()
        {
            // Arrange
            _mockRepository
                .Setup(repo => repo.GetProjectionsAsync(It.IsAny<Expression<Func<Projection, bool>>>()))
                .ReturnsAsync(new List<Projection>()); // Празен списък

            // Act
            var result = await _projectionService.GetProjectionIdByMovieCinemaAndShowtimeAsync(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now);

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}