using ApplicationCore.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Services
{
	public class AuthService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;

		public AuthService(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task Authenticate(User user)
		{
			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, user.Name),
				new Claim(Constants.UserId, user.Id.ToString())
			};

			ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

			await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
		}

		public Guid GetUserId(ClaimsPrincipal User)
		{
			Claim claimUserId = User.Claims.FirstOrDefault(x => x.Type == Constants.UserId);

			Guid.TryParse(claimUserId.Value, out Guid userId);

			return userId;
		}
	}
}
