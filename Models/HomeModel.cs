using RecipeLib.Entities;

namespace RecipeLib.Models
{
    public class HomeModel
    {
        public List<Recipe> recipes { get; set; }
        public List<Category> categories { get; set; }
    }
}