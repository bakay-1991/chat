using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels.Account;

namespace Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserRepository _userRepository;
		private readonly UserService _accountService;
		private readonly AuthService _authService;

		public AccountController(IUserRepository userRepository,
			UserService accountService,
			AuthService authService)
		{
			_userRepository = userRepository;
			_accountService = accountService;
			_authService = authService;
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _userRepository.GetAsync(model.Email);
				if (user == null)
				{
					//todo: we can configure AutoMapper and use here
					user = new User { Name = model.Name, Email = model.Email, Password = model.Password };
					await _accountService.CreateUserAsync(user);

					await _authService.Authenticate(user);

					return RedirectToAction("Index", "Chat");
				}
				ModelState.AddModelError("", "Incorrect username and(or) password");
			}
			return View(model);
		}

		[HttpGet]
		[AllowAnonymous]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User user = await _userRepository.GetAsync(model.Email, model.Password);
				if (user != null)
				{
					await _authService.Authenticate(user);

					return RedirectToAction("Index", "Chat");
				}
				ModelState.AddModelError("", "Incorrect username and(or) password");
			}
			return View(model);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Login", "Account");
		}
	}
}

