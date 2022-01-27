
using RecipeLib.Entities;

namespace RecipeLib.Models
{
    public class RecipeEditModel
    {
        public Recipe Recipe { get; set; }
        public List<Category> Categories { get; set; }
    }
}