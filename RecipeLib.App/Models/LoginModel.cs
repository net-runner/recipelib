using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;

namespace RecipeLib.Models;

public class LoginModel
{
    [Required]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "user name must be combination of letters and numbers only.")]
    [Display(Name = "Username")]
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}
