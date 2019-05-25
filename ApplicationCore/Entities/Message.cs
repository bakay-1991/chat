using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
	public class Message : BaseEntity
	{
		public string Text { get; set; }
		public DateTime Created { get; set; }

		public Guid FromUserId { get; set; }
		public User FromUser { get; set; }

		public Guid? ToUserId { get; set; }
		public User ToUser { get; set; }

		public Guid? ToGroupId { get; set; }
		public Group ToGroup { get; set; }
	}
}
