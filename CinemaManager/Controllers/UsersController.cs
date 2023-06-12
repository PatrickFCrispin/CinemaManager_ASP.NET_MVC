using CinemaManager.Models;
using CinemaManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetUsersAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        public async Task<IActionResult> ConfirmRemove(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserModel userModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var success = await _userRepository.CheckIfUserAlreadyExistsAsync(userModel);

                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailOrLoginAlreadyRegistered;

                        return View(userModel);
                    }

                    success = await _userRepository.AddUserAsync(userModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyCreated;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = MessagesModel.UnableToCreateUser;
                    }

                    return RedirectToAction("Index");
                }

                return View(userModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToCreateUser} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserWithoutPasswordModel userWithoutPasswordModel)
        {
            try
            {
                UserModel? userModel = null;

                if (ModelState.IsValid)
                {
                    userModel = new UserModel
                    {
                        Id = userWithoutPasswordModel.Id,
                        Name = userWithoutPasswordModel.Name,
                        Email = userWithoutPasswordModel.Email,
                        Login = userWithoutPasswordModel.Login,
                        Perfil = userWithoutPasswordModel.Perfil
                    };

                    var success = await _userRepository.CheckIfUserAlreadyExistsAsync(userModel);

                    if (success)
                    {
                        TempData["ErrorMessage"] = MessagesModel.EmailOrLoginAlreadyRegistered;

                        return View(userModel);
                    }

                    success = await _userRepository.UpdateUserAsync(userModel);

                    if (success)
                    {
                        TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyUpdated;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = MessagesModel.UnableToUpdateUser;
                    }

                    return RedirectToAction("Index");
                }

                return View(userModel);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToUpdateUser} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var success = await _userRepository.RemoveUserAsync(id);

                if (success)
                {
                    TempData["SuccessMessage"] = MessagesModel.UserSuccesfulyRemoved;
                }
                else
                {
                    TempData["ErrorMessage"] = MessagesModel.UnableToRemoveUser;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"{MessagesModel.UnableToRemoveUser} Erro: {ex.Message}";

                return RedirectToAction("Index");
            }
        }
    }
}