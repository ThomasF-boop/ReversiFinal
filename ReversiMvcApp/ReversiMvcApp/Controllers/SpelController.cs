using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using ReversiMvcApp.Service;
using System.Security.Claims;

namespace ReversiMvcApp.Controllers
{
    public class SpelController : Controller
    {
        private readonly ReversiDbContext context;
        private readonly ReversiApi api;

        public SpelController(ReversiDbContext context, ReversiApi api)
        {
            this.context = context;
            this.api = api;
        }

        public async Task<ActionResult> Index()
        {
            var games = await api.GetSpellenMetWachtendeSpeler();
            return View(games);
        }


        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Spel spel)
        {
            
           
                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                spel.Speler1Token = currentUserID;

                var createGame = await api.MaakNieuwSpel(spel);
                if (!createGame)
                {
                    ModelState.AddModelError("", "Something went wrong while creating the game, try again?");
                    return View(spel);
                }
                return RedirectToAction(nameof(Index));
            

        }

        public async Task<IActionResult> Details(string token)
        {
            if(token == null)
            {
                return NotFound();
            }

            var spel = await api.GetSpelByToken(token);
            return View(spel);
        }

    }
}
