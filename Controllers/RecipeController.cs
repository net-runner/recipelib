
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;

namespace RecipeLib.Controllers;




[Authorize(Roles = "Administrator, RecipeMaster")]
public class RecipeController : Controller
{
    private readonly ILogger<RecipeController> _logger;
    private readonly AppDbContext _dbContext;

    public RecipeController(ILogger<RecipeController> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Details(string id = "x")
    {
        _logger.LogInformation("Show recipe details: " + id);
        if (id == "x")
        {
            return LocalRedirect("/");
        }
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        return View(recipe);
    }
    public async Task<IActionResult> Delete(string id = "x")
    {
        _logger.LogInformation("Remove recipe: " + id);
        if (id == "x")
        {
            return LocalRedirect("/");
        }
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        _dbContext.Recipes.Remove(recipe);
        await _dbContext.SaveChangesAsync();
        return LocalRedirect("/");
    }
}
