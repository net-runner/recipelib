
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RecipeLib.Entities;

namespace RecipeLib.Models
{
    public class RecipeEditModel
    {
        public Recipe Recipe { get; set; }
        public List<Category> Categories { get; set; }

        [Required]
        [DisplayName("Upload image for details")]
        public IFormFile ImageSmallUpload { get; set; }


        [Required]
        [DisplayName("Upload image for homescreen card")]
        public IFormFile ImageCardUpload { get; set; }
    }
}