using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeLib.Entities;

namespace RecipeLib.Controllers;




[Route("api/")]
[ApiController]
public class ApiController : ControllerBase
{

    private readonly AppDbContext _dbContext;
    private readonly ILogger<ApiController> _logger;

    public ApiController(AppDbContext dbContext, ILogger<ApiController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
    {
        _logger.LogInformation("Api GetAllRecipes");
        return await _dbContext.Recipes.ToListAsync(CancellationToken.None);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipe>> GetRecipeById(string id)
    {
        _logger.LogInformation("Api GetRecipeById: " + id);
        var recipe = await _dbContext.Recipes.FindAsync(id);

        if (recipe is null)
        {
            return NotFound();
        }
        return recipe;
    }


    [HttpGet("category/{id}")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByCategoryId(string id)
    {
        _logger.LogInformation("Api GetRecipesByCategoryId: " + id);
        var recipe = await _dbContext.Recipes.Where(r => r.CategoryId == id).ToListAsync();

        if (recipe is null)
        {
            return BadRequest();
        }
        return recipe;
    }

    [HttpGet("author/{id}")]
    public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipesByAuthorId(string id)
    {
        _logger.LogInformation("Api GetRecipesByAuthorId: " + id);
        var recipe = await _dbContext.Recipes.Where(r => r.AuthorId == id).ToListAsync();

        if (recipe is null)
        {
            return BadRequest();
        }
        return recipe;
    }


    [HttpDelete("delete/{id}")]
    [Authorize(Roles = "Administrator")]
    [Authorize(Roles = "RecipeMaster")]
    public async Task<IActionResult> DeleteRecipe(string id)
    {
        _logger.LogInformation("Api DeleteRecipe: " + id);
        var recipe = await _dbContext.Recipes.FindAsync(id);
        if (recipe is null)
        {
            return NotFound();
        }
        _dbContext.Recipes.Remove(recipe);
        await _dbContext.SaveChangesAsync();
        return NoContent();
    }

}
