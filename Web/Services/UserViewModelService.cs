using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Services
{
	public class UserViewModelService
	{
		private readonly IRepository<User> _userRepository;

		public UserViewModelService(IRepository<User> userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<List<SelectListItem>> GetUsers(Guid currentUserId)
		{
			BaseSpecification<User> spec = new BaseSpecification<User>();
			spec.AddCriteria(x => x.Id != currentUserId);
			IReadOnlyList<User> users = await _userRepository.ListAsync(spec);

			return users.Select(x => new SelectListItem(x.Name, x.Id.ToString())).ToList();
		}
	}
}
