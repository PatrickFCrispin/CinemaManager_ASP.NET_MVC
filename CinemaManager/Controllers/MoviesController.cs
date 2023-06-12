using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _moviesRepository.GetMoviesAsync();
            return View(movies);
        }

        public async Task<IActionResult> IndexForUser()
        {
            var movies = await _moviesRepository.GetMoviesAsync();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var movie = await _moviesRepository.GetMovieByIdAsync(id);
            return View(movie);
        }

        public async Task<IActionResult> ConfirmRemove(int id)
        {
            var movie = await _moviesRepository.GetMovieByIdAsync(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MoviesModel moviesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var success = await _moviesRepository.CheckIfMovieIsAlreadyRegisteredAsync(moviesModel);

                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.MovieAlreadyRegistered;

                        return View(moviesModel);
                    }

                    success = await _moviesRepository.AddMovieAsync(moviesModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.MovieSuccesfulyCreated;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = MessagesModel.UnableToCreateMovie;
                    }

                    return RedirectToAction("Index");
                }

                return View(moviesModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToCreateMovie} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(MoviesModel moviesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var success = await _moviesRepository.CheckIfMovieIsAlreadyRegisteredAsync(moviesModel);

                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.MovieAlreadyRegistered;

                        return View(moviesModel);
                    }

                    success = await _moviesRepository.UpdateMovieAsync(moviesModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.MovieSuccesfulyUpdated;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = MessagesModel.UnableToUpdateMovie;
                    }

                    return RedirectToAction("Index");
                }

                return View(moviesModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToUpdateMovie} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var success = await _moviesRepository.CheckIfMovieIsLinkedToAnySessionAsync(id);

                if (success)
                {
                    TempData["ErrorMessage"] = MessagesModel.MovieLinkedToASession;

                    return RedirectToAction("Index");
                }

                success = await _moviesRepository.RemoveMovieAsync(id);

                if (success)
                {
                    TempData["SuccessMessage"] = MessagesModel.MovieSuccesfulyRemoved;
                }
                else
                {
                    TempData["ErrorMessage"] = MessagesModel.UnableToRemoveMovie;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToRemoveMovie} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}