using Microsoft.AspNetCore.Identity;

namespace RecipeLib.Entities;

public class User : IdentityUser
{
    public virtual ICollection<Recipe> Recipes { get; set; }
}