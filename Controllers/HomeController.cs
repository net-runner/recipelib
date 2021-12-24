using Microsoft.AspNetCore.Mvc;
using RecipeLib.Models;
using RecipeLib.Services;

namespace RecipeLib.Controllers;

[Route("/")]
public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}