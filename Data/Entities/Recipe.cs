using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace RecipeLib.Entities;

public class Recipe
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    [Required]
    public string Name { get; set; }

    [Required]
    public string ImgSmall { get; set; }
    [Required]
    public string ImgCard { get; set; }
    [Required]
    public int kcal { get; set; }


    [Required]
    public string CategoryId { get; set; }
    [Required]
    public string AuthorId { get; set; }

    [Required]
    public virtual Category Category { get; set; }






    [Required]
    [ForeignKey("AuthorId")]
    public virtual User User { get; set; }


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

    [NotMapped]
    public virtual List<Ingredient> Ingredients
    {
        get
        {
            return _Ingredients;
        }
        set
        {
            _Ingredients = value;
        }
    }

    [NotMapped]
    private List<Ingredient> _Ingredients { get; set; }

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

    public string IngredientsSerialized
    {
        get
        {
            return JsonConvert.SerializeObject(_Ingredients);
        }
        set
        {
            _Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(value);
        }
    }
}

public class Ingredient
{
    public string name { get; set; }

    public string ammount { get; set; }

}