using Microsoft.AspNetCore.Mvc;
using Alpha.Models.ViewModels;
using Alpha.Models;
using Microsoft.AspNetCore.Identity;

//Saker gick fel så bad chatgpt att rätta min kod.

namespace Alpha.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //GET Register
        [HttpGet]
        public IActionResult Register() => View();

        //POST Register 
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Console.WriteLine(" Register form submitted");

            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid");

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    Console.WriteLine("User created");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Project");
                }

                Console.WriteLine("Failed to create user:");
                foreach (var error in result.Errors)
                {
                    Console.WriteLine($"⚠️ {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                Console.WriteLine("Model is invalid");
            }

            return View(model);
        }


        //GET Login 
        [HttpGet]
        public IActionResult Login() => View();

        //POST Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Project");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }

            return View(model);
        }

        //Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}

