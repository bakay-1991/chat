using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
	public class Group : BaseEntity
	{
		public string Name { get; set; }

		public List<GroupUser> GroupUsers { get; set; } = new List<GroupUser>();
	}
}
