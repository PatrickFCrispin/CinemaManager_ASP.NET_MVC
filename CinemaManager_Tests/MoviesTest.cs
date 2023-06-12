using CinemaManager.Data;
using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CinemaManager.Tests
{
    public class MoviesTest
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesTest()
        {
            _moviesRepository = new MoviesRepository(
                new DBContext(new DbContextOptions<DBContext>()));
        }

        [Fact]
        public async Task TestCase01_OnGetMovies_IfCollectionOfMoviesIsEmpty_ShouldReturnEmptyAsync()
        {
            var movies = await _moviesRepository.GetMoviesAsync();
            Assert.Empty(movies);
        }

        [Fact]
        public async Task TestCase02_OnAddMovie_WithAllRequiredProperties_ShouldBeAddedAndReturnTrueAsync()
        {
            var movie = new MoviesModel
            {
                Image = "N/A",
                Title = "11 homens e um segredo",
                Description = "Sem descrição no momento.",
                Duration = "1h 45min"
            };

            var isMovieAdded = await _moviesRepository.AddMovieAsync(movie);
            Assert.True(isMovieAdded);
        }

        [Fact]
        public async Task TestCase03_OnGetMovies_IfCollectionOfMoviesIsNotEmpty_ShouldReturnNotEmptyAsync()
        {
            var movies = await _moviesRepository.GetMoviesAsync();
            Assert.NotEmpty(movies);
        }

        [Fact]
        public async Task TestCase04_OnGetMovieById_IfMovieIsAdded_ShouldReturnMovieAsync()
        {
            // Buscar por um ID que exista na base de dados
            var movie = await _moviesRepository.GetMovieByIdAsync(1);
            Assert.Equal(1, movie.Id);
        }

        [Fact]
        public async Task TestCase05_OnUpdateMovie_WithAllRequiredProperties_ShouldBeUpdatedAndReturnTrueAsync()
        {
            // Atualizar um ID que exista na base de dados
            var movie = await _moviesRepository.GetMovieByIdAsync(1);
            movie.Description = "11 ladrões profisionais que planejam perfeito.";

            var isMovieUpdated = await _moviesRepository.UpdateMovieAsync(movie);
            Assert.True(isMovieUpdated);
        }

        [Fact]
        public async Task TestCase06_OnAddOrUpdateMovie_IfMovieIsAlreadyRegistered_ShouldNotBeAddedOrUpdatedAndReturnTrueAsync()
        {
            // Não pode existir títulos duplicados.
            var newMovie = new MoviesModel
            {
                Image = "N/A",
                Title = "11 homens e um segredo",
                Description = "Sem descrição no momento",
                Duration = "2h"
            };

            var isMovieAlreadyRegistered = await _moviesRepository.CheckIfMovieIsAlreadyRegisteredAsync(newMovie);
            Assert.True(isMovieAlreadyRegistered);
        }

        [Fact]
        public async Task TestCase07_OnRemoveMovie_IfMovieIsNotLinkedToASession_ShouldReturnFalseAsync()
        {
            // Buscar por um ID que exista na base de dados
            var isAnySessionLinkedToThisMovie = await _moviesRepository.CheckIfMovieIsLinkedToAnySessionAsync(1);
            Assert.False(isAnySessionLinkedToThisMovie);
        }

        [Fact]
        public async Task TestCase08_OnRemoveMovie_IfMovieIsNotLinkedToASession_ShouldRemoveAndReturnTrueAsync()
        {
            // Remove um ID que exista na base de dados e que não esteja vinculado a nenhuma sessão
            // Pode ser usado o método anterior para buscar filmes que podem ser removidos
            var isMovieRemoved = await _moviesRepository.RemoveMovieAsync(1);
            Assert.True(isMovieRemoved);
        }
    }
}