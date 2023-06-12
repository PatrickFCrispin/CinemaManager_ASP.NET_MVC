using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionRepository _sessionRepository;

        public SessionController(ISessionRepository sessionRepository)
        {
            _sessionRepository = sessionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var sessions = await _sessionRepository.GetSessionsAsync();
            return View(sessions);
        }

        public async Task<IActionResult> IndexForUser()
        {
            var sessions = await _sessionRepository.GetSessionsAsync();
            return View(sessions);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> ConfirmRemove(int id)
        {
            var movie = await _sessionRepository.GetSessionByIdAsync(id);
            return View(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SessionModel sessionModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var success = _sessionRepository.CheckIfSessionStartDateIsValid(sessionModel);

                    if (!success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.InvalidSessionStartDate;

                        return View(sessionModel);
                    }

                    success = _sessionRepository.CheckIfMovieExists(sessionModel.MovieTitle);

                    if (!success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.MovieNotFound;

                        return View(sessionModel);
                    }

                    success = _sessionRepository.CheckIfTheaterExists(sessionModel.TheaterName);

                    if (!success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.TheaterNotFound;

                        return View(sessionModel);
                    }

                    success = await _sessionRepository.CheckIfSessionIsAlreadyAllocatedAsync(sessionModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.SessionAlreadyAllocated;
                    }

                    success = await _sessionRepository.AddSessionAsync(sessionModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.SessionSuccesfulyCreated;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = MessagesModel.UnableToCreateSession;
                    }

                    return RedirectToAction("Index");
                }

                return View(sessionModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToCreateSession} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var success = await _sessionRepository.CheckIfSessionCanBeRemovedAsync(id);

                if (!success)
                {
                    TempData["ErrorMessage"] = MessagesModel.SessionCannotBeRemoved;

                    return RedirectToAction("Index");
                }

                success = await _sessionRepository.RemoveSessionAsync(id);

                if (success)
                {
                    TempData["SuccessMessage"] = MessagesModel.SessionSuccesfulyRemoved;
                }
                else
                {
                    TempData["ErrorMessage"] = MessagesModel.UnableToRemoveSession;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToRemoveSession} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}