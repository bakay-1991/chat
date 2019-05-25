using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
	public class MessageLast : BaseEntity
	{
		public DateTime Created { get; set; }

		public Guid MessageId { get; set; }
		public Message Message { get; set; }

		public Guid? UserId { get; set; }
		public User User { get; set; }

		public Guid? GroupId { get; set; }
		public Group Group { get; set; }
	}
}
