using System.ComponentModel.DataAnnotations;

namespace RecipeLib.Entities;

public class Category
{
    [Required]
    public string Id { get; set; }
    [Required]
    public string Name { get; set; }

    public List<Recipe> Recipes { get; set; }
}