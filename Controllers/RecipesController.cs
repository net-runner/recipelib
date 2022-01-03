
using Microsoft.AspNetCore.Mvc;
namespace RecipeLib.Controllers;



public class RecipesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Welcome()
    {
        ViewData["Message"] = "Your welcome message";

        return View();
    }
}
