using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RecipeLib.Entities;

public class Recipe
{
    public string Id { get; set; }
    public string Name { get; set; }

    public string CategoryId { get; set; }

    public string AuthorId { get; set; }


    [NotMapped]
    public virtual List<string> Method
    {
        get
        {
            return _Method;
        }
        set
        {
            _Method = value;
        }
    }

    [NotMapped]
    private List<string> _Method;

    public List<Ingredient> Ingredients { get; set; }

    public string MethodSerialized
    {
        get
        {
            return JsonConvert.SerializeObject(_Method);
        }
        set
        {
            _Method = JsonConvert.DeserializeObject<List<string>>(value);
        }
    }
}

public class Ingredient
{
    public string name { get; set; }

    public string ammount { get; set; }

}