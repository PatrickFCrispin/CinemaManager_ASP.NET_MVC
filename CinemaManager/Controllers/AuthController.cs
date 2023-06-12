using CinemaManager.Helpers;
using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public AuthController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }

        public IActionResult Index()
        {
            var userSession = _userSession.GetUserSession();

            if (userSession is null) { return View(); }

            if (userSession.Perfil == Enums.PerfilEnum.Admin)
            {
                return RedirectToAction("Index", "Movies");
            }

            return RedirectToAction("IndexForUser", "Movies");
        }

        [HttpPost]
        public async Task<IActionResult> AttemptToSignIn(AuthModel authModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userRepository.GetUserByLoginAsync(authModel.Login);

                    if (user is not null)
                    {
                        if (user.ValidatePasswordFor(authModel.Password))
                        {
                            _userSession.CreateUserSession(user);

                            if (user.Perfil == Enums.PerfilEnum.Admin)
                            {
                                return RedirectToAction("Index", "Movies");
                            }

                            return RedirectToAction("IndexForUser", "Movies");
                        }
                    }

                    TempData["ErrorMessage"] = $"{MessagesModel.InvalidCredentials}";

                    return View("Index", authModel);
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToSignIn} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }

        public new IActionResult SignOut()
        {
            _userSession.RemoveUserSession();

            return RedirectToAction("Index", "Auth");
        }
    }
}