using System;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
	public class ChatViewModelService
	{
		private readonly ReceiverViewModelService _receiverViewModelService;
		private readonly MessageViewModelService _messageViewModelService;

		public ChatViewModelService(ReceiverViewModelService receiverViewModelService,
			MessageViewModelService messageViewModelService)
		{
			_receiverViewModelService = receiverViewModelService;
			_messageViewModelService = messageViewModelService;
		}

		public async Task<ChatViewModel> GetChatViewModel(Guid senderId, int pageIndex)
		{
			ChatViewModel result = new ChatViewModel();

			result.Receivers = await _receiverViewModelService.GetReceivers(senderId, pageIndex);
			if (result.Receivers.Count == 0)
			{
				return result;
			}

			Guid receiverId = result.Receivers.FirstOrDefault().Id;
			result.Messages = await _messageViewModelService.GetMessages(senderId, receiverId, pageIndex);

			return result;
		}
	}
}
