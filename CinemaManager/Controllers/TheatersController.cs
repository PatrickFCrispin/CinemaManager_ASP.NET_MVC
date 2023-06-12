using CinemaManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CinemaManager.Controllers
{
    public class TheatersController : Controller
    {
        private readonly ITheatersRepository _theatersRepository;

        public TheatersController(ITheatersRepository theatersRepository)
        {
            _theatersRepository = theatersRepository;
        }

        public async Task<IActionResult> Index()
        {
            var theaters = await _theatersRepository.GetTheatersAsync();
            return View(theaters);
        }
    }
}