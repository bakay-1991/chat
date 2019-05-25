using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
	public class GroupUser : BaseEntity
	{
		public Guid GroupId { get; set; }
		public Group Group { get; set; }

		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
