using Api.DAL;
using Api.Services.SupplierCRUD;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<ISupplierService, SupplierService>();

			services.AddScoped<IDAOSupplier, DAOSupplier>();

			return services;
		}
	}
}
