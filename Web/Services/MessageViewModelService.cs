using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
	public class MessageViewModelService
	{
		private readonly IRepository<Message> _messageRepository;
		private readonly IRepository<Group> _groupRepository;

		public MessageViewModelService(IRepository<Message> messageRepository,
			IRepository<Group> groupRepository)
		{
			_messageRepository = messageRepository;
			_groupRepository = groupRepository;
		}

		public async Task<List<MessageViewModel>> GetMessages(Guid senderId, Guid receiverId, int pageIndex)
		{
			Group group = await _groupRepository.GetByIdAsync(receiverId);

			BaseSpecification<Message> messageSpec;
			if (group != null)
			{
				messageSpec = new BaseSpecification<Message>();
				messageSpec.AddCriteria(x => x.ToGroupId == receiverId);
			}
			else
			{
				messageSpec = new BaseSpecification<Message>();
				messageSpec.AddCriteria(x => x.FromUserId == senderId && x.ToUserId == receiverId || x.FromUserId == receiverId && x.ToUserId == senderId);
			}

			messageSpec.ApplyPaging(pageIndex * Constants.ItemsPerPage, Constants.ItemsPerPage);
			messageSpec.ApplyOrderByDescending(x => x.Created);
			messageSpec.AddInclude(x => x.FromUser);
			IReadOnlyList<Message> messagesOnPage = await _messageRepository.ListAsync(messageSpec);

			ChatViewModel result = new ChatViewModel();

			return messagesOnPage.Select(x => ConvertTo(x, senderId, receiverId)).ToList();
		}

		public static MessageViewModel ConvertTo(Message message, Guid? senderId, Guid receiverId)
		{
			return new MessageViewModel()
			{
				ReceiverId = receiverId,
				Text = message.Text,
				Created = message.Created,
				Sender = message.FromUser.Id != senderId ? message.FromUser.Name : null,
			};
		}
	}
}
