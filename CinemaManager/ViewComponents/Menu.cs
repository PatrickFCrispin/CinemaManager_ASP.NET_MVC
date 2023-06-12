using CinemaManager.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CinemaManager.ViewComponents
{
    public class Menu : ViewComponent
    {
        public IViewComponentResult? Invoke()
        {
            var userSession = HttpContext.Session.GetString("UserSession");
            if (string.IsNullOrEmpty(userSession)) { return null; }

            var user = JsonConvert.DeserializeObject<UserModel>(userSession);

            return View("Menu", user);
        }
    }
}