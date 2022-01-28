
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


        if (rem.Recipe.ImgSmall == null)
        {
            rem.Recipe.ImgSmall = recipe.ImgSmall;
        }

        if (rem.Recipe.ImgCard == null)
        {
            rem.Recipe.ImgCard = recipe.ImgCard;

        }
        if (rem.Recipe.Method == null)
        {
            rem.Recipe.Method = recipe.Method;
        }

        if (rem.Recipe.Ingredients == null)
        {
            rem.Recipe.Ingredients = recipe.Ingredients;
        }


        _dbContext.Recipes.Remove(recipe);
        rem.Recipe.AuthorId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
        await _dbContext.Recipes.AddAsync(rem.Recipe);
        await _dbContext.SaveChangesAsync();

        ViewData["message"] = "Recipe edited successfully.";
        return LocalRedirect("/");
    }

    [HttpPost]
    private async Task<IActionResult> UploadRecipeImages(List<IFormFile> images)
    {

        _logger.LogInformation("Upload recipe images ** ");
        List<string> uploadedImages = new List<string>();

        if (images != null)
        {
            foreach (IFormFile image in images)
            {
                string uploadsFolder = Path.Combine(Environment.WebRootPath, "/assets/recipe-img");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }


        }
        return Ok(uploadedImages);
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

    public async Task<IActionResult> IngredientRow(string id)
    {
        _logger.LogInformation("Add IngredientRow");
        var recipe = await _dbContext.Recipes.FindAsync(id);
        var categories = _dbContext.Categories.ToList();
        recipe.Ingredients.Add(new Ingredient());
        return View("Edit", new RecipeEditModel() { Recipe = recipe, Categories = categories });
    }
}
