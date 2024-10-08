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

            var AllGames = await api.GetAllSpellen();


            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value; // Example, adjust based on your auth logic

            // Filter ongoing games where the player is involved
            var ongoingGames = AllGames.Where(g => g.Finished == false &&
                                                  (g.Speler1Token == currentUserID || g.Speler2Token == currentUserID)).ToList();

            // Pass both the list of ongoing games and all games to the view
            ViewBag.OngoingGames = ongoingGames;


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

            string createGame = await api.MaakNieuwSpel(spel);
            if (string.IsNullOrEmpty(createGame))
            {
                ModelState.AddModelError("", "Something went wrong while creating the game, try again?");
                return View(spel);
            }

            return RedirectToAction("Details", "Spel", new { token = createGame });
            

        }

        public async Task<IActionResult> Details(string token)
        {
            if(token == null)
            {
                return NotFound();
            }

            var spel = await api.GetSpelBySpelerToken(token);

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var spelerToken = currentUserID;

            var spelSpeler = new GamePlayerToken()
            {
                Token = spel.Token,
                PlayerToken = spelerToken
            };
            return View(spelSpeler);
        }

        public async Task<IActionResult> Join(string token)
        {
            if (token == null)
            {
                return NotFound();
            }
            var spel = await api.GetSpelByToken(token);

            ClaimsPrincipal currentUser = this.User;
            var spelerToken = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var result = await api.JoinSpel(token, spelerToken);

            if (result)
            {
                return RedirectToAction("Details", "Spel", new {token = spel.Token});
            }

            return BadRequest("Failed to join game, try again?");
        }

        public async Task<IActionResult> Continue(string token)
        {
            if (token == null)
            {
                return NotFound();
            }

            var spel = await api.GetSpelByToken(token);
       
            return RedirectToAction("Details", "Spel", new { token = spel.Token });

           
        }

        public async Task<IActionResult> Result(string token)
        {
            if (token == null)
            {
                return NotFound();
            }

            var spel = await api.GetSpelByToken(token);

            if(spel.Finished)
            {
                Speler speler1 = context.Speler.FirstOrDefault(s => s.GUID == spel.Speler1Token);
                if (speler1 == null)
                {
                    return NotFound();
                }

                Speler speler2 = context.Speler.FirstOrDefault(s => s.GUID == spel.Speler2Token);
                if (speler1 == null)
                {
                    return NotFound();
                }

                ClaimsPrincipal currentUser = this.User;
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                if (currentUserID != spel.Winnaar)
                {
                    return View(spel);
                }

                if(spel.Winnaar == speler1.GUID)
                {  
                    speler1.AantalGewonnen++;
                    speler2.AantalVerloren++;

                }else if (spel.Winnaar == speler2.GUID)
                {
                    speler2.AantalGewonnen++;
                    speler1.AantalVerloren++;
                }
                else
                {
                    speler2.AantalGelijk++;
                    speler1.AantalGelijk++;
                }
                spel.PuntenGegeven = true;

                context.SaveChanges();

               await api.DeleteGame(spel.Token, token);
            }

            return View(spel);
        }

    }
}
