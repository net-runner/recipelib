using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using RecipeLib.Entities;

namespace RecipeLib;

public class RecipeAppSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher<User> passwordHasher;

    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public RecipeAppSeeder(AppDbContext dbContext, IPasswordHasher<User> passwordHasher, UserManager<User> userManager, RoleManager<Role> roleManager)
    {
        _dbContext = dbContext;
        this.passwordHasher = passwordHasher;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async void CreateInitialData()
    {
        if (_dbContext.Database.CanConnect())
        {
            //Create initial roles

            if (!_dbContext.Roles.Any())
            {
                await CreateRoles();

                //Create initial users
                if (!_dbContext.Users.Any())
                {
                    await CreateUsers();
                }
            }


            //Create initial recipe categories
            if (!_dbContext.Categories.Any())
            {
                List<Category> categories = new List<Category>(){

                new Category() { Name = "Breakfast", Id = Guid.NewGuid().ToString() },
                new Category() { Name = "Main Dishes", Id = Guid.NewGuid().ToString()},
                new Category() { Name = "Side Dishes", Id = Guid.NewGuid().ToString()},
                new Category() { Name = "Snacks", Id = Guid.NewGuid().ToString() },
                new Category() { Name = "Beverages", Id = Guid.NewGuid().ToString() },
                new Category() { Name = "Desserts", Id = Guid.NewGuid().ToString() },

                };
                await CreateCategories(categories);

                //Create initial recipes
                if (!_dbContext.Recipes.Any())
                {
                    await CreateRecipes(categories);
                }
            }

        }
    }
    private async Task CreateRecipes(List<Category> categories)
    {
        var InitialAuthor = await _userManager.GetUsersInRoleAsync("Administrator");

        List<Ingredient> DefaultIngredients = new List<Ingredient>(){
            new Ingredient(){
                name = "Large Egg",
                ammount="1"
            },
            new Ingredient(){
                name = "Peanut Butter",
                ammount="250g"
            },
                        new Ingredient(){
                name = "Erythritol",
                ammount="50g"
            },
        };
        List<string> DefaultMethod = new List<string>(){
            "Preheat oven to 180°C",
            "In a medium bowl combine all ingredients until thoroughly mixed",
            "Scoop heaping one tablespoon-sized piece of dough into 12-15 balls. Place 2 inches apart on lined cookie sheets and flatten in a criss-cross with a fork",
            "Bake for 12-15 minutes until edges of cookies are golden brown",
            "Let cool completely before eating."
        };
        List<Recipe> recipes = new List<Recipe>(){

            new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe01",
                CategoryId = categories[0].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe02",
                CategoryId = categories[0].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe03",
                CategoryId = categories[0].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe11",
                CategoryId = categories[1].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe12",
                CategoryId = categories[1].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe13",
                CategoryId = categories[1].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                                    new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe21",
                CategoryId = categories[2].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe22",
                CategoryId = categories[2].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe23",
                CategoryId = categories[2].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                                                new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe31",
                CategoryId = categories[3].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe32",
                CategoryId = categories[3].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe33",
                CategoryId = categories[3].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                                                new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe41",
                CategoryId = categories[4].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe42",
                CategoryId = categories[4].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },
                        new Recipe(){
                Id = Guid.NewGuid().ToString(),
                Name = "Recipe43",
                CategoryId = categories[4].Id,
                AuthorId = InitialAuthor[0].Id,
                Ingredients = DefaultIngredients,
                Method = DefaultMethod
            },

        };

        await _dbContext.AddRangeAsync(recipes, CancellationToken.None);
        await _dbContext.SaveChangesAsync();
    }
    private async Task CreateRoles()
    {
        List<Role> roles = new List<Role>(){
new Role() { Name = "Administrator" },
new Role() { Name = "User" },
new Role() { Name = "RecipeMaster" }
        };
        await _roleManager.CreateAsync(roles[0]);
        await _roleManager.CreateAsync(roles[1]);
        await _roleManager.CreateAsync(roles[2]);

    }
    private async Task CreateUsers()
    {
        User u1, u2, u3;
        string pass = "zaq1@WSX";
        u1 = new User()
        {
            UserName = "Guest",
            Email = "guest@guest.guest",
        };
        u2 = new User()
        {
            UserName = "Adam",
            Email = "adam@theadmin.recipedb",

        };
        u3 = new User()
        {
            UserName = "Kuchta",
            Email = "kuchta@kuchta.kuchta",
        };

        await _userManager.CreateAsync(u1, pass);
        await _userManager.CreateAsync(u2, pass);
        await _userManager.CreateAsync(u3, pass);
        await _userManager.AddToRoleAsync(u1, "User");
        await _userManager.AddToRoleAsync(u2, "Administrator");
        await _userManager.AddToRoleAsync(u3, "RecipeMaster");
    }

    private async Task CreateCategories(List<Category> categories)
    {
        await _dbContext.AddRangeAsync(categories, CancellationToken.None);
        await _dbContext.SaveChangesAsync();
    }


}