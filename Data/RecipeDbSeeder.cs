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
            }
            //Create initial users
            if (!_dbContext.Users.Any())
            {
                await CreateUsers();
            }

        }
    }

    private async Task CreateRoles()
    {
        await _roleManager.CreateAsync(new Role() { Name = "Administrator" });
        await _roleManager.CreateAsync(new Role() { Name = "User" });
        await _roleManager.CreateAsync(new Role() { Name = "RecipeMaster" });

    }
    private async Task CreateUsers()
    {
        User u1, u2, u3;
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

        await _userManager.CreateAsync(u1, "zaq1@WSX");
        await _userManager.CreateAsync(u2, "zaq1@WSX");
        await _userManager.CreateAsync(u3, "zaq1@WSX");
    }
}