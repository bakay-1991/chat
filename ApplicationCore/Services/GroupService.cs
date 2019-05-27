using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
	public class GroupService
	{
		private readonly IRepository<Group> _groupRepository;

		public GroupService(IRepository<Group> groupRepository)
		{
			_groupRepository = groupRepository;
		}

		public async Task<Group> CreateGroupAsync(Guid currentUserId, string name, List<Guid> UserIds)
		{
			Guard.Against.NullOrEmpty(name, nameof(name));
			Guard.Against.NullOrEmpty(UserIds, nameof(UserIds));

			UserIds.Add(currentUserId);
			Group group = new Group() { Name = name, GroupUsers = UserIds.Select(x => new GroupUser() { UserId = x }).ToList() };

			await _groupRepository.AddAsync(group);

			return group;
		}
	}
}
