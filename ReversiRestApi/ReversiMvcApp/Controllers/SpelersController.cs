﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Controllers
{
    public class SpelersController : Controller
    {
        private readonly ReversiDbContext _context;

        public SpelersController(ReversiDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Create([Bind("GUID,Naam,AantalGewonnen,AaltalVerloren,AantalGelijk")] Speler speler)
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
        public async Task<IActionResult> Edit(string id, [Bind("GUID,Naam,AantalGewonnen,AaltalVerloren,AantalGelijk")] Speler speler)
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Speler == null)
            {
                return Problem("Entity set 'ReversiDbContext.Speler'  is null.");
            }
            var speler = await _context.Speler.FindAsync(id);
            if (speler != null)
            {
                _context.Speler.Remove(speler);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpelerExists(string id)
        {
            return (_context.Speler?.Any(e => e.GUID == id)).GetValueOrDefault();
        }
    }
}
