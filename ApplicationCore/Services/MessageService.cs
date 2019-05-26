using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Ardalis.GuardClauses;
using System;
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

		public async Task<Message> CreateMessageAsync(Guid userId, string text, Guid? ToUserId, Guid? ToGroupId)
		{
			Guard.Against.NullOrEmpty(text, nameof(text));
			if (ToUserId == null && ToGroupId == null)
			{
				throw new Exception("Missed receiver.");
			}

			Message message = new Message() { Created = DateTime.UtcNow, FromUserId = userId, Text = text, ToUserId = ToUserId, ToGroupId = ToGroupId };

			await _messageRepository.AddAsync(message);

			BaseSpecification<Message> messageSpec = new BaseSpecification<Message>();
			messageSpec.AddCriteria(x => x.Id == message.Id);
			messageSpec.AddInclude(x => x.FromUser);

			return await _messageRepository.FirstOrDefaultAsync(messageSpec);
		}
	}
}
