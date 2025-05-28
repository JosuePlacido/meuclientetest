using Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Api.Extensions;
using Api.ActionFilters;
using Swashbuckle.AspNetCore.Swagger;
using Api.Models;


namespace Api
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}


		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddApiVersioning(options =>
			{
				options.AssumeDefaultVersionWhenUnspecified = true;
				options.DefaultApiVersion = new ApiVersion(1, 0);
				options.ReportApiVersions = true;
			});
			services.AddMvcCore().AddVersionedApiExplorer(options =>
			{
				options.GroupNameFormat = "'v'VVV";
				options.AssumeDefaultVersionWhenUnspecified = true;
			});

			services.AddMvc(options =>
			{
				options.Filters.Add<ValidationExceptionFilter>();
			}).AddJsonOptions(options =>
			{
				options.SerializerSettings.Converters.Add(new CNPJSerialization());
			})
			.SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddDbContext<Context>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
			services.AddServices();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Title = "MeuClientTest API",
					Version = "v1"
				});
			});
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			// app.UseHttpsRedirection();
			app.UseMvc();

			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
				c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
			});
		}
	}
}
