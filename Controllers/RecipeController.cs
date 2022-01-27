
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeLib.Controllers;




[Authorize(Roles = "Administrator, RecipeMaster")]
public class RecipeController : Controller
{
    private readonly ILogger<RecipeController> _logger;
    private readonly AppDbContext _dbContext;

    private readonly UserManager<User> _userManager;

    private IWebHostEnvironment Environment;
    public RecipeController(ILogger<RecipeController> logger, AppDbContext dbContext, UserManager<User> userManager, IWebHostEnvironment environment)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userManager = userManager;
        Environment = environment;
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

    public async Task<IActionResult> Edit(string id = "x")
    {
        _logger.LogInformation("Edit recipe view: " + id);
        if (id == "x")
        {
            return LocalRedirect("/");
        }
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        var categories = _dbContext.Categories.ToList();
        return View("Edit", new RecipeEditModel() { Recipe = recipe, Categories = categories });
    }

    [HttpPost]
    public async Task<IActionResult> EditRecipe(RecipeEditModel rem, string id)
    {
        _logger.LogInformation("Editing recipe: " + id);
        _logger.LogInformation("Editing FR: " + rem.Recipe.Id);
        // _logger.LogInformation("Editing recipe: " + rem.Recipe.Id + "| Recipe name: " + rem.Recipe.Name + " | TargetID: " + id);
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe == null)
        {
            return NotFound();
        }
        _dbContext.Recipes.Remove(recipe);

        rem.Recipe.ImgCard = UploadRecipeImages(rem.Recipe.ImageCardUpload);
        rem.Recipe.ImgSmall = UploadRecipeImages(rem.Recipe.ImageSmallUpload);

        rem.Recipe.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        await _dbContext.Recipes.AddAsync(rem.Recipe);
        await _dbContext.SaveChangesAsync();

        ViewData["message"] = "Recipe edited successfully.";
        return LocalRedirect("/");
    }
    private string UploadRecipeImages(IFormFile image)
    {
        string uniqueFileName = null;

        if (image != null)
        {
            string uploadsFolder = Path.Combine(Environment.WebRootPath, "/assets/recipe-img");
            uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
        }
        return uniqueFileName;
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
