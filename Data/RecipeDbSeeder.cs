using Microsoft.AspNetCore.Identity;
using RecipeLib.Entities;

namespace RecipeLib;

public class RecipeAppSeeder
{
    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher<User> passwordHasher;

    public RecipeAppSeeder(AppDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        this.passwordHasher = passwordHasher;
    }

    public void CreateInitialData()
    {
        if (_dbContext.Database.CanConnect())
        {
            //Create initial roles
            if (!_dbContext.Roles.Any())
            {
                _dbContext.Roles.AddRange(InitialRoles());
                _dbContext.SaveChanges();
            }
            //Create initial users
            if (!_dbContext.Users.Any())
            {
                _dbContext.Users.AddRange(InitialUsers());
                _dbContext.SaveChanges();
            }


        }
    }

    private IEnumerable<Role> InitialRoles() => new List<Role>(){
        new Role(){
            Name = "Administrator"
        },
        new Role(){
            Name = "User"
        },
        new Role(){
            Name = "Recipe Master"
        },
    };
    private IEnumerable<User> InitialUsers()
    {
        User u1, u2, u3;
        u1 = new User()
        {
            Nickname = "Guest",
            Email = "guest@guest.guest",
            RoleId = 2
        };
        u2 = new User()
        {
            Nickname = "Adam",
            Email = "adam@theadmin.recipedb",
            RoleId = 1

        };
        u3 = new User()
        {
            Nickname = "Kuchta",
            Email = "kuchta@kuchta.kuchta",
            RoleId = 3
        };
        u1.PasswordHash = passwordHasher.HashPassword(u1, "zaq1@WSX");
        u2.PasswordHash = passwordHasher.HashPassword(u2, "zaq1@WSX");
        u3.PasswordHash = passwordHasher.HashPassword(u3, "zaq1@WSX");
        List<User> users = new List<User>() { u1, u2, u3 };
        return users;
    }
}