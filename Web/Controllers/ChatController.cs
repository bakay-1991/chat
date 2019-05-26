using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
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

		public IActionResult Privacy()
		{
			return View();
		}
	}
}
