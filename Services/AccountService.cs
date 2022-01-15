using Microsoft.AspNetCore.Identity;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeLib.Services;

public interface IAccountService
{
    void RegisterUser(RegisterModel dto);
}
public class AccountService : IAccountService
{

    private readonly AppDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    private readonly IUserStore<User> _userStore;
    private readonly IPasswordHasher<User> passwordHasher;
    private readonly ILogger<AccountService> _logger;

    private readonly SignInManager<User> _signInManager;

    public AccountService(IUserStore<User> userStore, UserManager<User> userManager, AppDbContext dbContext, IPasswordHasher<User> passwordHasher, SignInManager<User> signInManager, ILogger<AccountService> logger)
    {
        _dbContext = dbContext;
        _signInManager = signInManager;
        this.passwordHasher = passwordHasher;
        _logger = logger;
        _userManager = userManager;
        _userStore = userStore;
    }
    public void RegisterUser(RegisterModel dto)
    {
        _logger.LogInformation(dto.Username);
        _logger.LogInformation(dto.Password);

        var user = new User()
        {
            Email = dto.Email,
            UserName = dto.Username,
        };
        user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);
        _dbContext.Users.Add(user);

        _dbContext.SaveChanges();
    }


}