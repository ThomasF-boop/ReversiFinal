using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using ReversiMvcApp.Service;

namespace ReversiMvcApp.Controllers
{
    public class SpelersController : Controller
    {
        private readonly ReversiDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ReversiApi _api;

        public SpelersController(ReversiDbContext context, UserManager<IdentityUser> userManager, ReversiApi api)
        {
            _context = context;
            _userManager = userManager;
            _api = api;

        }

        // GET: Spelers
        public async Task<IActionResult> Index()
        {
              return _context.Speler != null ? 
                          View(await _context.Speler.ToListAsync()) :
                          Problem("Entity set 'ReversiDbContext.Speler'  is null.");
        }

        // GET: Spelers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            ClaimsPrincipal currentUser = this.User;
            var spelerToken = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;


            var speler = await _context.Speler
                .FirstOrDefaultAsync(m => m.GUID == spelerToken);
            if (speler == null)
            {
                return NotFound();
            }

            return View(speler);
        }

        // GET: Spelers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Spelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GUID,Naam,AantalGewonnen,AantalVerloren,AantalGelijk")] Speler speler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(speler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(speler);
        }

        // GET: Spelers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Speler == null)
            {
                return NotFound();
            }

            var speler = await _context.Speler.FindAsync(id);
            if (speler == null)
            {
                return NotFound();
            }
            return View(speler);
        }

        // POST: Spelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("GUID,Naam,AantalGewonnen,AantalVerloren,AantalGelijk")] Speler speler)
        {
            if (id != speler.GUID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(speler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpelerExists(speler.GUID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(speler);
        }

        // GET: Spelers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Speler == null)
            {
                return NotFound();
            }

            var speler = await _context.Speler
                .FirstOrDefaultAsync(m => m.GUID == id);
            if (speler == null)
            {
                return NotFound();
            }

            return View(speler);
        }

        // POST: Spelers/Delete/5
        

        private bool SpelerExists(string id)
        {
          return (_context.Speler?.Any(e => e.GUID == id)).GetValueOrDefault();
        }

        [Authorize(Roles = "Beheerder, Mediator")]
        public async Task<IActionResult> List()
        {
            var spelers = await _context.Speler.ToListAsync();
            return View(spelers);
        }

        // POST: Spelers/Delete/5
        // POST: Spelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Beheerder, Mediator")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Speler == null)
            {
                return Problem("Entity set 'ReversiDbContext.Speler' is null.");
            }

            // Find the speler
            var speler = await _context.Speler.FindAsync(id);
            if (speler == null)
            {
                return NotFound();
            }

            // Use ReversiApi to get games associated with the speler
            var games = await _api.GetAllSpellen(); // Assuming this method returns games where Speler1Token or Speler2Token matches speler.GUID

            // Delete each game associated with the speler
            foreach (var game in games)
            {
                if (game.Speler1Token == speler.GUID || game.Speler2Token == speler.GUID)
                {
                    var success = await _api.DeleteSpel(game.Token, speler.GUID);

                    if (!success)
                    {
                        // Handle failure to delete game (log, return error, etc.)
                        return Problem("Failed to delete associated game.");
                    }



                    // For now, assume the other player wins
                    var otherPlayerToken = game.Speler1Token == speler.GUID ? game.Speler2Token : game.Speler1Token;
                    var otherPlayer = await _context.Speler.FirstOrDefaultAsync(s => s.GUID == otherPlayerToken);

                    if (otherPlayer != null)
                    {
                        otherPlayer.AantalGewonnen++; // Increment wins for the other player
                    }
                }
            }

            // Remove the speler
            _context.Speler.Remove(speler);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
