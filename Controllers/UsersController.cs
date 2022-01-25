using Microsoft.AspNetCore.Mvc;

namespace RecipeApp.Controllers
{


    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}