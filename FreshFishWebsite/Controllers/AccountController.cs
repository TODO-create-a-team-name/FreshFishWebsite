using FreshFishWebsite.Interfaces.Registering;
using FreshFishWebsite.Models;
using FreshFishWebsite.Services;
using FreshFishWebsite.Services.ClaimsRegistration;
using FreshFishWebsite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FreshFishWebsite.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly FreshFishDbContext _context;
        private readonly IAccountRepository _repo;
        public AccountController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            FreshFishDbContext context,
            IAccountRepository repo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _repo = repo;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    Name = model.Name,
                    Usersurname = model.Surname,
                    Company = model.Company,
                    CompanyAddress = model.CompanyAddress
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action(
                        "ConfirmEmail",
                        "Account",
                        new { userId = user.Id, code = code },
                        protocol: HttpContext.Request.Scheme);

                    await new EmailService().SendEmailAsync(model.Email, "Підтвердіть свій акаунт",
                        $"Підтвердіть свій акаунт за наступним посиланням: <a href='{callbackUrl}'>Підтвердити акаунт</a>");

                    return Content("Для завершення реєстрації, перейдіть за посиланням, яке було надіслане вам на пошту.");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult RegisterStorageAdmin(int storageId, string adminEmail)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == storageId);
            var model = new RegisterStorageWorker();
            if (storage != null)
            {
                model.StorageId = storageId;
                model.StorageNumber = storage.StorageNumber;
                model.StorageAddress = storage.Address;
                model.WorkerEmail = adminEmail;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStorageAdmin(RegisterStorageWorker model)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.RegisterClaim(new StorageAdminRegistration(model, _context, _userManager));
                if (!result.Any())
                {
                    return RedirectToAction("Index", "Storage");
                }
                else
                {
                    foreach (var error in result)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult RegisterStorageDriver(int storageId, string email)
        {
            var storage = _context.Storages.FirstOrDefault(s => s.Id == storageId);
            var model = new RegisterStorageWorker();
            if (storage != null)
            {
                model.StorageId = storageId;
                model.StorageNumber = storage.StorageNumber;
                model.StorageAddress = storage.Address;
                model.WorkerEmail = email;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterStorageDriver(RegisterStorageWorker model)
        {
            if (ModelState.IsValid)
            {
                var result = await _repo.RegisterClaim(new DriverRegistration(model, _context, _userManager));

                if (!result.Any())
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
            => View(new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            });

        #region External login
        [AllowAnonymous]
        [HttpPost]
        //метод приймає два параметри: провайдер(Google, Facebook...), посилання для повернення
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",
                new { ReturnUrl = returnUrl }); //генеруємо посилання перенаправлення
            //генеруємо властивості для автентифікації
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);//відображаємо автентифікацію провайдера
        }

        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/"); //визначаємо посилання перенаправлення

            LoginViewModel loginViewModel = new LoginViewModel
            {
                ReturnUrl = returnUrl,
                ExternalLogin = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null) //якщо є помилки провайдера
            {
                //відображаємо помилки та здійснюємо перенаправлення на сторінку логування
                ModelState.AddModelError(string.Empty, $"Помилка зовнішнього провайдера {remoteError}");
                return View("Login", loginViewModel);
            }
            //отримуємо інформацію автентифікації
            var info = await _signInManager.GetExternalLoginInfoAsync();

            if (info == null) //якшо інформацію не вдалося завантажити
            {
                //відображаємо помилку і перенаправляємо користувача на сторінку логування
                ModelState.AddModelError(string.Empty, "Помилка завантаження інформації зовнішнього провайдера");

                return View("Login", loginViewModel);
            }
            //здійснюємо зовнішнє логування
            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider,
                info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            //якщо все вдалося
            if (signInResult.Succeeded)
            {
                //перенаправляємо користувача за посиланням перенаправлення
                return LocalRedirect(returnUrl);
            }
            else
            {
                //дістаємо електронну пошту
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);
                //якщо пошта є
                if (email != null)
                {
                    //шукаємо користувача за поштою
                    var user = await _userManager.FindByEmailAsync(email);
                    //якщо такого користувача немає
                    if (user == null)
                    {
                        //генеруємо нові дані користувача
                        user = new User
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Name = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Usersurname = info.Principal.FindFirstValue(ClaimTypes.Surname)
                        };
                        // і створюємо користувача в базі даних
                        await _userManager.CreateAsync(user);
                    }
                    //додаємо логування до таблиці AspNetLogins
                    await _userManager.AddLoginAsync(user, info);
                    //Автентифікуємо користувача
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //перенаправляємо на посилання перенаправлення
                    return LocalRedirect(returnUrl);
                }
                //якщо були помилки, то генеруємо їх
                ViewBag.ErrorTitle = $"Заява електронної пошти не була отримана від: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Будь ласка, зв'яжіться з підтримкою freshfishofficial@gmail.com";
                return View("Error");//і повертаємо сторінку помилок
            }
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user != null)
                {

                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {

                        ModelState.AddModelError(string.Empty, "Ви не підтвердили свою пошту!");
                        return View(model);
                    }
                }

                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Невірний логін або(і) пароль");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    return View("ForgotPasswordConfirmation");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);

                var callbackUrl = Url.Action("ResetPassword",
                    "Account",
                    new { userId = user.Id, code = code },
                    protocol: HttpContext.Request.Scheme);

                EmailService emailService = new EmailService();

                await emailService.SendEmailAsync(model.Email,
                    "Зміна паролю",
                    $"Перейдіть за наступним посиланням, щоб змінити ваш пароль:" +
                    $" <a href='{callbackUrl}'>Змінити пароль</a>");

                return View("ForgotPasswordConfirmation");
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return View("ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return View("ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }
    }
}
