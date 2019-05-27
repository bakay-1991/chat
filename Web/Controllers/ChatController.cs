using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Controllers
{
	[Authorize]
	public class ChatController : Controller
	{
		private readonly AuthService _authService;
		private readonly ChatViewModelService _chatViewModelService;

		public ChatController(AuthService authService,
			ChatViewModelService chatViewModelService)
		{
			_authService = authService;
			_chatViewModelService = chatViewModelService;
		}

		public async Task<IActionResult> Index()
		{
			Guid userId = _authService.GetUserId(User);
			return View(await _chatViewModelService.GetChatViewModel(userId, 0));
		}
	}
}
