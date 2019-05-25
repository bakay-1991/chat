using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Providers
{
	public class CustomUserIdProvider : IUserIdProvider
	{
		public virtual string GetUserId(HubConnectionContext connection)
		{
			Claim claimUserId = connection.User.Claims.FirstOrDefault(x => x.Type == "UserId");

			if (claimUserId != null && Guid.TryParse(claimUserId.Value, out Guid userId))
			{
				return userId.ToString();
			}

			return string.Empty;
		}
	}
}
