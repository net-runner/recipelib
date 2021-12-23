using Microsoft.AspNetCore.Identity;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeLib.Services;

public interface IAccountService
{
    void RegisterUser(RegisterUserDto dto);
}
public class AccountService : IAccountService
{

    private readonly AppDbContext _dbContext;
    private readonly IPasswordHasher<User> passwordHasher;

    public AccountService(AppDbContext dbContext, IPasswordHasher<User> passwordHasher)
    {
        _dbContext = dbContext;
        this.passwordHasher = passwordHasher;
    }
    public void RegisterUser(RegisterUserDto dto)
    {

        var user = new User()
        {
            Email = dto.Email,
            Nickname = dto.Nickname,
            RoleId = dto.RoleId,
        };
        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();
    }
}