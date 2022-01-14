using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RecipeLib.Entities
{
    public class AppDbContext : IdentityDbContext<User, Role, string>
    {

        private string _connectionString = "Data Source= localhost;User Id=recipeapp;password=zaq1@WSX;Trusted_Connection=True;MultipleActiveResultSets=true; Integrated Security = false; DATABASE=recipedb";
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

    }
}