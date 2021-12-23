using Microsoft.AspNetCore.Mvc;
using RecipeLib.Models;
using RecipeLib.Services;

namespace RecipeLib.Controllers;

[Route("/")]
public class MainController : ControllerBase
{

    public IActionResult Index()
    {
        return File("~/index.html", "text/html");
    }
}