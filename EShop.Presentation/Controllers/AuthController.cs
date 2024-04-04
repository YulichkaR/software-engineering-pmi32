using EShop.Domain.Models;
using EShop.Presentation.Models;
using EShop.Presentation.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace EShop.Presentation.Controllers;

public class AuthController : Controller
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly ILogger _logger;
    private readonly IUserStore<User> _userStore;
    private readonly IUserEmailStore<User> _emailStore;

    public AuthController(
        SignInManager<User> signInManager,
        UserManager<User> userManager,
        ILogger logger,
        IUserStore<User> userStore
    )
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _userStore = userStore;
        _emailStore = (IUserEmailStore<User>)userStore;
    }
    // GET
    public IActionResult Login()
    {
        _logger.Information("User is on the login page.");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("User entered invalid data.");
            return View();
        }
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
        if (result.Succeeded)
        {
            _logger.Information("User logged in.");
            return RedirectToAction("Index", "Product");
        }
        _logger.Error("User entered invalid credentials.");
        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View();
    }
    public IActionResult Register()
    {
        _logger.Information("User is on the register page.");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            _logger.Error("User entered invalid data.");
            return View();
        }

        var user = new User();
        await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
        await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            _logger.Information("User registered.");
            await _userManager.AddToRoleAsync(user, "User");
            _logger.Information("User added to User role.");
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Product");
        }
        _logger.Error("User entered invalid data.");
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        _logger.Information("User logged out.");
        return RedirectToAction("Index", "Product");
    }
}