using System;

namespace ApplicationCore.Entities
{
	public class BaseEntity
	{
		public Guid Id { get; set; } = Guid.NewGuid();
	}
}
