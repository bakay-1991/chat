using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
	public class GroupViewModel
	{
		[Required]
		[Display(Name = "Name")]
		public string Name { get; set; }

		[Required]
		[Display(Name = "Users")]
		public List<Guid> UserIds { get; set; }
	}
}
