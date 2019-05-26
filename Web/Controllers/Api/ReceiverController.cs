using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;
using Web.ViewModels;

namespace Web.Controllers.Api
{
	public class ReceiverController : BaseApiController
	{
		private readonly AuthService _authService;
		private readonly ReceiverViewModelService _receiverViewModelService;

		public ReceiverController(AuthService authService,
			ReceiverViewModelService receiverViewModelService)
		{
			_authService = authService;
			_receiverViewModelService = receiverViewModelService;
		}

		[HttpGet]
		public async Task<IActionResult> Get(int page)
		{
			Guid userId = _authService.GetUserId(User);
			List<ReceiverViewModel> receivers = await _receiverViewModelService.GetReceivers(userId, page);
			return Ok(receivers);
		}
	}
}
