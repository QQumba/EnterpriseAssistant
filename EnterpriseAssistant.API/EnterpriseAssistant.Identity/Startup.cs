using EnterpriseAssistant.Identity.DataAccess;
using EnterpriseAssistant.Identity.Requirements;
using EnterpriseAssistant.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace EnterpriseAssistant.Identity
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityServer()
				.AddInMemoryIdentityResources(Config.IdentityResources)
				.AddInMemoryApiScopes(Config.ApiScopes)
				.AddInMemoryClients(Config.Clients)
				.AddProfileService<ProfileService>()
				.AddDeveloperSigningCredential();
			
			services.AddControllersWithViews();

			services.AddApi();
			services.AddDataAccess(Configuration);
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());

			app.UseIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action}");
			});

			app.Use(async (context, next) =>
			{
				await next();
				if (context.Response.StatusCode == 404)
				{
					context.Response.Redirect("https://localhost:5002/swagger");
				}
			});
		}
	}
}
