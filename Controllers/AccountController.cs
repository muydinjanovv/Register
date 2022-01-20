using System.Text.Json;
using register.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using register.ViewModels;

namespace intro.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<User> _userM;
    private readonly SignInManager<User> _signInM;
    private readonly ILogger<AccountController> _logger;

    public AccountController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<AccountController> logger)
    {
        _userM = userManager;
        _signInM = signInManager;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Register(string returnUrl)
    {
        return View(new RegisterViewModel() { ReturnUrl = returnUrl ?? string.Empty });
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        var user = new User()
        {
            Fullname = model.Fullname,
            Email = model.Email,
            UserName = model.Username,
            Birthdate = model.Birthdate,
            PhoneNumber = model.Phone
        };

        var result = await _userM.CreateAsync(user, model.Password);

        if(result.Succeeded)
        {
            return LocalRedirect($"~/account/login?returnUrl={model.ReturnUrl}");
        }

        return BadRequest(JsonSerializer.Serialize(result.Errors));
    }

    [HttpGet]
    public  IActionResult Login(string returnUrl)
    {
        _logger.LogWarning("get login");
        return View(new LoginViewModel() { ReturnUrl  = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        _logger.LogWarning($"New email: {model.Email}");
        var user = await _userM.FindByEmailAsync(model.Email);
        if(user != null)
        {
            await _signInM.SignInAsync(user, false); // isPersistant

            return LocalRedirect($"~/Home/Authorization");
        }

        return BadRequest("Wrong credentials email exists");
    }
}