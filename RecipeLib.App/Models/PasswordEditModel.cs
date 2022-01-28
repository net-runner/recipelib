using System.ComponentModel.DataAnnotations;

namespace RecipeLib.Models;

public class PasswordEditModel
{

    public string Id { get; set; }

    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

}
