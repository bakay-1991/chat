using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
	public class ChatContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Group> Groups { get; set; }
		public DbSet<GroupUser> GroupUsers { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<MessageLast> MessageLasts { get; set; }

		public ChatContext(DbContextOptions<ChatContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
						.HasIndex(x => x.Email)
						.IsUnique();

			modelBuilder.Entity<GroupUser>()
						.HasIndex(x => new { x.UserId, x.GroupId })
						.IsUnique();

			modelBuilder.Entity<MessageLast>()
						.HasIndex(x => x.UserId)
						.IsUnique();

			modelBuilder.Entity<MessageLast>()
						.HasIndex(x => x.GroupId)
						.IsUnique();

			User user1 = new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Name = "user1", Email = "user1@gmail.com", Password = "123456" };
			User user2 = new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000002"), Name = "user2", Email = "user2@gmail.com", Password = "123456" };
			User user3 = new User { Id = Guid.Parse("00000000-0000-0000-0000-000000000003"), Name = "user3", Email = "user3@gmail.com", Password = "123456" };

			modelBuilder.Entity<User>()
						.HasData(new User[] { user1, user2, user3 });

			Group group1 = new Group { Id = Guid.Parse("00000000-0000-0000-0000-000000000004"), Name = "Group 1" };
			Group group2 = new Group { Id = Guid.Parse("00000000-0000-0000-0000-000000000005"), Name = "Group 2" };

			modelBuilder.Entity<Group>()
						.HasData(new Group[] { group1, group2 });

			GroupUser group1User1 = new GroupUser { Id = Guid.Parse("00000000-0000-0000-0000-000000000006"), GroupId = group1.Id, UserId = user1.Id };
			GroupUser group1User2 = new GroupUser { Id = Guid.Parse("00000000-0000-0000-0000-000000000007"), GroupId = group1.Id, UserId = user2.Id };
			GroupUser group1User3 = new GroupUser { Id = Guid.Parse("00000000-0000-0000-0000-000000000008"), GroupId = group1.Id, UserId = user3.Id };

			GroupUser group2User2 = new GroupUser { Id = Guid.Parse("00000000-0000-0000-0000-000000000009"), GroupId = group2.Id, UserId = user2.Id };
			GroupUser group2User3 = new GroupUser { Id = Guid.Parse("00000000-0000-0000-0000-000000000010"), GroupId = group2.Id, UserId = user3.Id };

			modelBuilder.Entity<GroupUser>()
						.HasData(new GroupUser[] { group1User1, group1User2, group1User3, group2User2, group2User3 });

			base.OnModelCreating(modelBuilder);
		}
	}
}
