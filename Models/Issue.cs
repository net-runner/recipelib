
namespace RecipeLib.Models
{
    public class Issue
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public DateTime Date { get; set; }

        public Contact Contact { get; set; }
    }
}