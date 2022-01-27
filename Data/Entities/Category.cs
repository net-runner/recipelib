namespace RecipeLib.Entities;

public class Category
{
    public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<Recipe> Recipes { get; set; }
}