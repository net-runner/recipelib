using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeApp.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {

        private readonly ILogger<UsersController> _logger;
        private readonly AppDbContext _dbContext;

        private readonly UserManager<User> _userManager;

        public UsersController(AppDbContext dbContext, UserManager<User> userManager, ILogger<UsersController> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            return View("Index", users);
        }


        public async Task<IActionResult> Edit(string id = "x")
        {
            _logger.LogInformation("Show edit: " + id);
            if (id == "x")
            {
                return LocalRedirect("/Users");
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        public async Task<IActionResult> EditUser(User user)
        {
            if (ModelState.IsValid)
            {
                var usington = await _userManager.FindByIdAsync(user.Id);
                await _userManager.SetEmailAsync(usington, user.Email);
                await _userManager.SetUserNameAsync(usington, user.UserName);
            }
            return LocalRedirect("/Users");
        }

        public async Task<IActionResult> EditPass(string id = "x")
        {
            _logger.LogInformation("Change pass: " + id);
            if (id == "x")
            {
                return LocalRedirect("/Users");
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(new PasswordEditModel() { Id = id, Password = "", Username = user.UserName });
        }



        public async Task<IActionResult> DoEditPass(PasswordEditModel p)
        {
            _logger.LogInformation("DoChange pass: " + p.Id + " | Pass: " + p.Password);
            if (ModelState.IsValid)
            {
                var usington = await _userManager.FindByIdAsync(p.Id);
                await _userManager.RemovePasswordAsync(usington);
                await _userManager.AddPasswordAsync(usington, p.Password);
            }
            return LocalRedirect("/Users");
        }


        public async Task<IActionResult> Delete(string id = "x")
        {
            _logger.LogInformation("Remove user: " + id);
            if (id == "x")
            {
                return LocalRedirect("/Users");
            }
            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.DeleteAsync(user);
            return LocalRedirect("/Users");
        }
    }
}