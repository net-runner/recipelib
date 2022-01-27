using Microsoft.AspNetCore.Identity;

namespace RecipeLib.Entities;

public class User : IdentityUser
{
    public List<Recipe> Recipes { get; set; }
}