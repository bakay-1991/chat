using Microsoft.AspNetCore.SignalR;
using System;
using Web.Services;

namespace Web.Providers
{
	public class CustomUserIdProvider : IUserIdProvider
	{
		private readonly AuthService _authService;

		public CustomUserIdProvider(AuthService authService)
		{
			_authService = authService;
		}

		public virtual string GetUserId(HubConnectionContext connection)
		{
			Guid userId = _authService.GetUserId(connection.User);

			return userId.ToString();
		}
	}
}
