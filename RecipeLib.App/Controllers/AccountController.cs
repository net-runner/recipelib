using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecipeLib.Entities;
using RecipeLib.Models;

namespace RecipeLib.Controllers;


public class AccountController : Controller
{

    private readonly ILogger<AccountController> _logger;

    private readonly SignInManager<User> _signInManager;

    private readonly UserManager<User> _userManager;

    private readonly AppDbContext _dbContext;

    private readonly IUserStore<User> _userStore;

    private readonly IUserEmailStore<User> _emailStore;
    private readonly RoleManager<Role> _roleManager;


    public AccountController(ILogger<AccountController> logger, SignInManager<User> signInManager, IUserStore<User> userStore, UserManager<User> userManager, AppDbContext dbContext, RoleManager<Role> roleManager)
    {
        _logger = logger;
        _signInManager = signInManager;
        _userStore = userStore;
        _userManager = userManager;
        _emailStore = GetEmailStore();
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    public IActionResult Login()
    {
        return View();
    }
    public IActionResult Register()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> RegisterUser(RegisterModel dto)
    {
        _logger.LogInformation("User onRegister post");
        if (ModelState.IsValid)
        {
            _logger.LogInformation(dto.Username);
            _logger.LogInformation(dto.Password);
            var user = new User();
            await _userStore.SetUserNameAsync(user, dto.Username, CancellationToken.None);
            await _emailStore.SetEmailAsync(user, dto.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, dto.Password);

            await _userManager.AddToRoleAsync(user, "User");
            if (result.Succeeded)
            {

                _logger.LogInformation("User created a new account with password.");
                await _signInManager.SignInAsync(user, isPersistent: false);
                return LocalRedirect("/");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View("Register");

    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LoginUser(LoginModel dto)
    {
        _logger.LogInformation("User LogIn post");
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, dto.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect("/");
            }
            else
            {
                _logger.LogInformation("User login failed.");
                ModelState.AddModelError(string.Empty, "Invalid username or password");
            }
        }
        return View("Login");
    }
    public IActionResult Logout()
    {
        _signInManager.SignOutAsync();
        return LocalRedirect("/");
    }


    public async Task<JsonResult> UsernameExists(string username, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Check username---");
        var result = await _userStore.FindByNameAsync(username, cancellationToken);


        if (result is null)
        {
            _logger.LogInformation("--not found");
            return Json(true);
        }
        _logger.LogInformation("--found");
        return Json(false);
    }
    private IUserEmailStore<User> GetEmailStore()
    {
        if (!_userManager.SupportsUserEmail)
        {
            throw new NotSupportedException("The default UI requires a user store with email support.");
        }
        return (IUserEmailStore<User>)_userStore;
    }

    public async Task<JsonResult> EmailExists(string email, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Check email---");
        var result = await _emailStore.FindByEmailAsync(email, cancellationToken);


        if (result is null)
        {
            _logger.LogInformation("--not found");
            return Json(true);
        }
        _logger.LogInformation("--found");
        return Json(false);
    }
}