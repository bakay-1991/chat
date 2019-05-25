using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
	class ChatContextFactory : IDesignTimeDbContextFactory<ChatContext>
	{
		public ChatContext CreateDbContext(string[] args)
		{
			DbContextOptionsBuilder<ChatContext> optionsBuilder = new DbContextOptionsBuilder<ChatContext>();
			optionsBuilder.UseSqlServer("Data Source = .; Initial Catalog = chatdb; Integrated Security = True;");

			return new ChatContext(optionsBuilder.Options);
		}
	}
}
