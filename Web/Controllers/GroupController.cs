using ApplicationCore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers
{
	[Authorize]
	public class GroupController : Controller
	{
		private readonly AuthService _authService;
		private readonly UserViewModelService _userViewModelService;
		private readonly GroupService _groupService;

		public GroupController(AuthService authService,
			UserViewModelService userViewModelService,
			GroupService groupService)
		{
			_authService = authService;
			_userViewModelService = userViewModelService;
			_groupService = groupService;
		}

		public async Task<IActionResult> Index()
		{
			Guid currentUserId = _authService.GetUserId(User);
			await InitUsers(currentUserId);
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Index(GroupViewModel model)
		{
			Guid currentUserId = _authService.GetUserId(User);
			await InitUsers(currentUserId);

			if (ModelState.IsValid)
			{
				await _groupService.CreateGroupAsync(currentUserId, model.Name, model.UserIds);
			}
			return View();
		}

		private async Task InitUsers(Guid currentUserId)
		{			
			ViewBag.Users = await _userViewModelService.GetUsers(currentUserId);
		}
	}
}
