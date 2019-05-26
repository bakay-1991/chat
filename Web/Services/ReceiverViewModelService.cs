using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using ApplicationCore.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.ViewModels;

namespace Web.Services
{
	public class ReceiverViewModelService
	{
		private readonly IRepository<Group> _groupRepository;
		private readonly IRepository<User> _userRepository;

		public ReceiverViewModelService(IRepository<Group> groupRepository,
			IRepository<User> userRepository)
		{
			_groupRepository = groupRepository;
			_userRepository = userRepository;
		}

		public async Task<List<ReceiverViewModel>> GetReceivers(Guid currentUserId, int pageIndex)
		{
			BaseSpecification<Group> groupSpec = new BaseSpecification<Group>();
			groupSpec.AddCriteria(x => x.GroupUsers.Any(y => y.UserId == currentUserId));
			groupSpec.ApplyPaging(pageIndex * Constants.ItemsPerPage, Constants.ItemsPerPage);
			groupSpec.ApplyOrderBy(x => x.Name);
			IReadOnlyList<Group> groups = await _groupRepository.ListAsync(groupSpec);

			List<ReceiverViewModel> results = groups.Select(x => new ReceiverViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				IconUrl = "https://ptetutorials.com/images/user-profile.png" //todo: need add to db
			}).ToList();

			if (Constants.ItemsPerPage == results.Count)
			{
				return results;
			}

			int groupCount = await _groupRepository.CountAsync(new BaseSpecification<Group>());
			int gropupPage = (int)Math.Floor((decimal)(groupCount / Constants.ItemsPerPage));
			pageIndex -= gropupPage;
			int skip = pageIndex > 0 ? (((gropupPage + 1) * Constants.ItemsPerPage) - groupCount) + ((pageIndex - 1) * Constants.ItemsPerPage) : 0;
			BaseSpecification<User> userSpec = new BaseSpecification<User>();
			userSpec.AddCriteria(x => x.Id != currentUserId);
			userSpec.ApplyPaging(skip, Constants.ItemsPerPage - results.Count);
			userSpec.ApplyOrderBy(x => x.Name);
			IReadOnlyList<User> users = await _userRepository.ListAsync(userSpec);

			results.AddRange(users.Select(x => new ReceiverViewModel()
			{
				Id = x.Id,
				Name = x.Name,
				IconUrl = "https://ptetutorials.com/images/user-profile.png" //todo: need add to db
			}).ToList());

			return results;
		}
	}
}
