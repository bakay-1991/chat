using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Hubs
{
	public class ChatHub : Hub
	{
		private readonly MessageService _messageService;
		private readonly IRepository<Group> _groupRepository;
		private readonly IRepository<GroupUser> _groupUserRepository;

		public ChatHub(MessageService messageService,
			 IRepository<Group> groupRepository,
			 IRepository<GroupUser> groupUserRepository)
		{
			_messageService = messageService;
			_groupRepository = groupRepository;
			_groupUserRepository = groupUserRepository;
		}

		public async Task Notify(Guid receiverId, string message)
		{
			if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
			{
				Group group = await _groupRepository.GetByIdAsync(receiverId);
				if (group != null)
				{
					await _messageService.CreateMessageAsync(userId, receiverId, message);
					await Clients.Group(group.Id.ToString()).SendAsync("Receive", receiverId, message);
				}
				else
				{
					await _messageService.CreateMessageAsync(userId, receiverId, message);
					await Clients.User(receiverId.ToString()).SendAsync("Receive", receiverId, message);
				}
			}
		}

		public override async Task OnConnectedAsync()
		{
			if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
			{
				BaseSpecification<GroupUser> spec = new BaseSpecification<GroupUser>(x => x.UserId == userId);
				//spec.AddInclude(x => x.Group);
				IReadOnlyList<GroupUser> groupUsers = await _groupUserRepository.ListAsync(spec);
				foreach (GroupUser groupUser in groupUsers)
				{
					await Groups.AddToGroupAsync(Context.ConnectionId, groupUser.GroupId.ToString());
				}
			}
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
			{
				BaseSpecification<GroupUser> spec = new BaseSpecification<GroupUser>(x => x.UserId == userId);
				//spec.AddInclude(x => x.Group);
				IReadOnlyList<GroupUser> groupUsers = await _groupUserRepository.ListAsync(spec);
				foreach (GroupUser groupUser in groupUsers)
				{
					await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupUser.GroupId.ToString());
				}
			}
			await base.OnDisconnectedAsync(exception);
		}
	}
}
