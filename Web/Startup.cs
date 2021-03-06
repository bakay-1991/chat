﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Hubs;
using Web.Providers;
using Web.Services;

namespace Web
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			string connection = Configuration.GetConnectionString("ChatConnection");
			services.AddDbContext<ChatContext>(options => options.UseSqlServer(connection));

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = new PathString("/Account/Login");
					options.AccessDeniedPath = new PathString("/Account/Login");
				});

			services.AddSignalR();

			services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();
			services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<UserService>();
			services.AddSingleton<AuthService>();
			services.AddScoped<MessageService>();
			services.AddScoped<GroupService>();
			services.AddScoped<MessageViewModelService>();
			services.AddScoped<ReceiverViewModelService>();
			services.AddScoped<ChatViewModelService>();
			services.AddScoped<UserViewModelService>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

			services.AddHttpContextAccessor();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();

			app.UseAuthentication();

			app.UseSignalR(routes =>
			{
				routes.MapHub<ChatHub>("/chat");
			});

			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "default",
					template: "{controller=Chat}/{action=Index}/{id?}");
			});
		}
	}
}
