using System;

namespace Web.ViewModels
{
	public class MessageViewModel
	{
		public Guid ReceiverId { get; set; }
		public string Text { get; set; }
		public DateTime Created { get; set; }
		public string Sender { get; set; }
	}
}
