using System.Collections.Generic;

namespace Web.ViewModels
{
	public class ChatViewModel
	{
		public List<MessageViewModel> Messages { get; set; } = new List<MessageViewModel>();
		public List<ReceiverViewModel> Receivers { get; set; } = new List<ReceiverViewModel>();
	}
}
