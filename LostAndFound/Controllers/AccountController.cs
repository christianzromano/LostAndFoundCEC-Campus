using LostAndFound.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LostAndFound.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string studentId, string password)
        {
            if(string.IsNullOrWhiteSpace(studentId) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Student ID and Password are required.";
                return View();
            }

            var user = _context.Users.FirstOrDefault(u => u.StudentId == studentId && u.Password == password);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.FullName),
                    new Claim(ClaimTypes.NameIdentifier, user.StudentId),
                    new Claim(ClaimTypes.Role, user.Role)
                };

                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true
                };

                await HttpContext.SignInAsync("Cookies", new ClaimsPrincipal(claimsIdentity), authProperties);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Authentication failed, show error message
                ViewBag.ErrorMessage = "Invalid Student ID or Password.";
                return View();
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string studentId, string fullName, string course, string password)
        {
            if (_context.Users.Any(u => u.StudentId == studentId))
            {
                ViewBag.ErrorMessage = "Student ID already exists.";
                return View();
            }
            var newUser = new Models.Users
            {
                StudentId = studentId.Trim(),
                FullName = fullName.Trim(),
                Course = course.Trim(),
                Password = password,
                Role = "Member"
            };
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            ViewBag .SuccessMessage = "Registration successful. Please log in.";
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Login");
        }
    }
}
