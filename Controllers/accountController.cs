using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReceitaMx.Requests;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ReceitaMx.Controllers;

public class accountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public accountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    
    
    [HttpGet("login")]
    public async Task<IActionResult> Login()
    {
        /*
            var result = await _roleManager.CreateAsync(new() { Name = "Admin" });
            var admin = await _userManager.FindByEmailAsync("matheus@gmail.com");
            await _userManager.AddToRoleAsync(admin, "Admin");
        */
        return View("Login");
    }

    [Route("logout")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    
    [HttpPost]
    public async Task<IActionResult> LoginRequest(LoginRequest request)
    {
        SignInResult result;
        if (request.Logued)
        {
             result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, true, false);
        }
        else
        {
            result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, false);
        }

        if (result.Succeeded) {
            return RedirectToAction("Index", "Home");
        }
        return RedirectToAction("Login");
    }

    
    
    [Route("register")]
    public IActionResult Register()
    {
        return View("Register");
    }

    [HttpPost]
    public async Task<IActionResult> RegisterRequest(RegisterRequest request)
    {
        var result = await _userManager.CreateAsync(new() { UserName = request.UserName, Email = request.Email}, request.Password);
        if (result.Succeeded)
        {
            // var roleres = await _roleManager.CreateAsync(new() { Name = "Member" });
            // if (roleres.Succeeded)
            // {
                var user = await _userManager.FindByEmailAsync(request.Email);
                var task = await _userManager.AddToRoleAsync(user, "Member");
                if (task.Succeeded)
                {
                    var claims = await _userManager.AddClaimsAsync(user, new []
                    {
                        new Claim("USER_ID", user.Id),
                        new Claim("USER_EMAIL", user.Email),
                        new Claim("USER_NORMALIZADE_EMAIL", user.NormalizedEmail),
                        new Claim("USER_NAME", user.UserName),
                        new Claim("USER_NORMALIZADE_NAME", user.NormalizedUserName)
                    });
                    if (claims.Succeeded)
                    {
                        return RedirectToAction("Dashboard");
                    }
                }
             // }
        }
        return RedirectToAction("Register");
    }
    
    

    [Route("dashboard")]
    [Authorize(Roles = "Admin")]
    public IActionResult Dashboard()
    {
        return View("Dashboard");
    }
    
    
    
}
