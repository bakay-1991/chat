using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers.Api
{
	public class MessageController : BaseApiController
	{
		private readonly AuthService _authService;
		private readonly MessageViewModelService _messageViewModelService;

		public MessageController(AuthService authService,
			MessageViewModelService messageViewModelService)
		{
			_authService = authService;
			_messageViewModelService = messageViewModelService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(Guid receiverId, int page)
		{
			Guid userId = _authService.GetUserId(User);
			List<MessageViewModel> messages = await _messageViewModelService.GetMessages(userId, receiverId, page);
			return Ok(messages);
		}
	}
}
