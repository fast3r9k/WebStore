using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _UserManager;
        private readonly SignInManager<User> _SignInManager;
        private readonly ILogger<AccountController> _Logger;

        public AccountController(UserManager<User> UserManager, SignInManager<User> SignInManager, ILogger<AccountController> Logger)
        {

            _UserManager = UserManager;
            _SignInManager = SignInManager;
            _Logger = Logger;
        }

        #region Register
        [AllowAnonymous]
        public IActionResult Register() => View(new RegisterUserViewModel());

        [HttpPost, ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterUserViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            _Logger.LogInformation("Register new user {0}", Model.UserName);

            using (_Logger.BeginScope("New User Registration "))
            {
                var user = new User
                {
                    UserName = Model.UserName,
                };

                var registerResult = await _UserManager.CreateAsync(user, Model.Password);
                if (registerResult.Succeeded)
                {

                    _Logger.LogInformation("Register succeed");

                    await _UserManager.AddToRoleAsync(user, Role.User);

                    _Logger.LogInformation("The user {0} is assigned the {1} role", user.UserName, Role.User);

                    await _SignInManager.SignInAsync(user, false);

                    _Logger.LogInformation("The user has login");

                    return RedirectToAction("Index", "Home");
                }

                _Logger.LogWarning("User registration error {0}:{1}", user.UserName,
                    string.Join(",", registerResult.Errors.Select(e => e.Description)));

                foreach (var error in registerResult.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(Model);
        }

        #endregion

        #region Login
        [AllowAnonymous]
        public IActionResult Login(string ReturnUrl) => View(new LoginViewModel { ReturnUrl = ReturnUrl });

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel Model)
        {
            if (!ModelState.IsValid) return View(Model);

            _Logger.LogInformation("Log in {0} ", Model.UserName);

            var loginResult = await _SignInManager.PasswordSignInAsync(
                Model.UserName,
                Model.Password,
                Model.RememberMe,
#if DEBUG
               false
#else
                true
#endif
                );

            if (loginResult.Succeeded)
            {
                _Logger.LogInformation("Login {0} was succed", Model.UserName);

                if (Url.IsLocalUrl(Model.ReturnUrl))
                    return Redirect(Model.ReturnUrl);
                return RedirectToAction("Index", "Home");
            }

            _Logger.LogWarning("Login {0} failed. Username or Password is incorrect", Model.UserName);

            ModelState.AddModelError("", "The username or password you entered is incorrect");
            return View(Model);
        }
        #endregion

        public async Task<IActionResult> Logout()
        {
            var userName = User.Identity.Name;
            await _SignInManager.SignOutAsync();

            _Logger.LogInformation("{0} was logout", userName);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();
    }
}
