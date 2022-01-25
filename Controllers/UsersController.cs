using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;

namespace RecipeApp.Controllers
{

    [Authorize(Roles = "Administrator")]
    public class UsersController : Controller
    {

        private readonly AppDbContext _dbContext;

        public UsersController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var users = _dbContext.Users.ToList();
            return View("Index", users);
        }

    }
}