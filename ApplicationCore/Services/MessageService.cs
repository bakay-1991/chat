using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class MessageService
	{
		private readonly IRepository<Message> _messageRepository;
		private readonly IRepository<User> _userRepository;
		private readonly IRepository<Group> _groupRepository;

		public MessageService(IRepository<Message> messageRepository,
			IRepository<User> userRepository,
			IRepository<Group> groupRepository)
		{
			_messageRepository = messageRepository;
			_userRepository = userRepository;
			_groupRepository = groupRepository;
		}

		public async Task CreateMessageAsync(Guid userId, Guid receiverId, string text)
		{
			Guard.Against.NullOrEmpty(text, nameof(text));

			Message message = new Message() { Created = DateTime.UtcNow, FromUserId = userId, Text = text };

			User user = await _userRepository.GetByIdAsync(receiverId);
			if (user != null)
			{
				message.ToUserId = user.Id;
			}
			else
			{
				Group group = await _groupRepository.GetByIdAsync(receiverId);
				message.ToGroupId = group?.Id;
			}

			await _messageRepository.AddAsync(message);
		}
	}
}
