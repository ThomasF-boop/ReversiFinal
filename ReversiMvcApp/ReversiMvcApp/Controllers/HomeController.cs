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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger,ReversiDbContext context,RoleManager<IdentityRole> roleManager,UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;


        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            // Ensure the user exists in the database
            var existingUser = _context.Speler.FirstOrDefault(s => s.GUID == currentUserID);

            if (existingUser == null)
            {
                // Create a new Speler record with the user ID
                Speler newSpeler = new Speler
                {
                    GUID = currentUserID,
                    Naam = currentUser.Identity.Name
                // Set other properties as needed
            };

                _context.Speler.Add(newSpeler);
                _context.SaveChanges();
            }

            // Check if the user exists in the AspNetUsers table
            var user = await _userManager.FindByIdAsync(currentUserID);
            if (user != null)
            {
                var roles = await _userManager.GetRolesAsync(user);

                // Check if the user is already in the "Speler" role
                if (!roles.Any())
                {
                    await _userManager.AddToRoleAsync(user, "Speler");
                }
            }

            return View();
        }

        [Authorize(Roles = "Beheerder")]
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