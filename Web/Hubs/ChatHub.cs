using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Services;

namespace Web.Hubs
{
	[Authorize]
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
					Message msg = await _messageService.CreateMessageAsync(userId, message, null, group.Id);
					//todo: need to save ConnectionId in DB
					await Clients.Client(Context.ConnectionId).SendAsync("Receive", MessageViewModelService.ConvertTo(msg, userId, receiverId));
					await Clients.GroupExcept(group.Id.ToString(), Context.ConnectionId).SendAsync("Receive", MessageViewModelService.ConvertTo(msg, null, receiverId));
				}
				else
				{
					Message msg = await _messageService.CreateMessageAsync(userId, message, receiverId, null);
					await Clients.User(userId.ToString()).SendAsync("Receive", MessageViewModelService.ConvertTo(msg, userId, receiverId));
					await Clients.User(receiverId.ToString()).SendAsync("Receive", MessageViewModelService.ConvertTo(msg, receiverId, userId));
				}
			}
		}

		public override async Task OnConnectedAsync()
		{
			if (Guid.TryParse(Context.UserIdentifier, out Guid userId))
			{
				BaseSpecification<GroupUser> spec = new BaseSpecification<GroupUser>();
				spec.AddCriteria(x => x.UserId == userId);
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
				BaseSpecification<GroupUser> spec = new BaseSpecification<GroupUser>();
				spec.AddCriteria(x => x.UserId == userId);
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
