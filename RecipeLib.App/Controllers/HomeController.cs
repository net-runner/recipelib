using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeLib.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly AppDbContext _dbContext;

    public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    public IActionResult Index()
    {
        var recipes = _dbContext.Recipes.ToList();
        var categories = _dbContext.Categories.ToList();
        return View("Index", new HomeModel() { recipes = recipes, categories = categories });
    }

    public IActionResult Privacy()
    {
        return View();
    }
}