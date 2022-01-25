
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RecipeLib.Controllers;



[Authorize(Roles = "Administrator")]
[Authorize(Roles = "RecipeMaster")]
public class RecipesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Delete()
    {
        ViewData["Message"] = "Your welcome message";

        return View();
    }
}
