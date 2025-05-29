using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Extensions
{
	public static class SwaggerCollectionExtensions
	{
		public static IServiceCollection AddVersionedSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				var provider = services.BuildServiceProvider()
									   .GetRequiredService<IApiVersionDescriptionProvider>();

				foreach (var description in provider.ApiVersionDescriptions)
				{
					c.SwaggerDoc(description.GroupName, new Info
					{
						Title = $"MeuClientTest API {description.ApiVersion}",
						Version = description.GroupName
					});
					c.DocumentFilter<ReplaceVersionWithExactValueInPath>();
					c.OperationFilter<RemoveVersionFromParameter>();
					c.OperationFilter<RenameTagOperationFilter>();
				}
			});
			return services;
		}
	}
	public class RenameTagOperationFilter : IOperationFilter
	{
		private static IDictionary<string, string> Tags = new Dictionary<string, string>()
		{
			{"Asset","Ativo"},
			{"TypeAsset","Tipo de Ativo"},
			{"Order","Contrato de venda"},
			{"Supplier","Fornecedor"},
		};
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var controllerName = context.ApiDescription.ActionDescriptor.RouteValues["controller"];
			operation.Tags[0] = Tags[controllerName];
		}
	}
	public class ReplaceVersionWithExactValueInPath : IDocumentFilter
	{
		public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
		{
			var newPaths = new Dictionary<string, PathItem>();

			foreach (var (key, value) in swaggerDoc.Paths)
			{
				var newKey = key.Replace("v{version}", swaggerDoc.Info.Version);

				newPaths.Add(newKey, value);
			}

			swaggerDoc.Paths = newPaths;
		}
	}
	public class RemoveVersionFromParameter : IOperationFilter
	{
		public void Apply(Operation operation, OperationFilterContext context)
		{
			var versionParameter = operation.Parameters?.FirstOrDefault(p => p.Name == "version" || p.Name == "api-version");
			if (versionParameter != null)
				operation.Parameters.Remove(versionParameter);
		}
	}
}
