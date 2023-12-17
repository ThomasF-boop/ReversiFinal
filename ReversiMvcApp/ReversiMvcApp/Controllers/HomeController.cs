using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReversiMvcApp.Data;
using ReversiMvcApp.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace ReversiMvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReversiDbContext _context;

        public HomeController(ILogger<HomeController> logger,ReversiDbContext context)
        {
            _logger = logger;
            _context = context;
         
        }
        [Authorize]
        public  IActionResult Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var existingUser = _context.Speler.FirstOrDefault(s => s.GUID == currentUserID);

            if (existingUser == null)
            {
     
                // Create a new Speler record with the user ID
                Speler newSpeler = new Speler
                {
                    GUID = currentUserID,
                    // Set other properties as needed
                };

                _context.Speler.Add(newSpeler);
                _context.SaveChanges();
            }

            return View();
        }
        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}